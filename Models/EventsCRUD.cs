using EventRegSystem.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace EventRegSystem.Models
{
    public class EventsCRUD
    {
        private AppDbContext db;
        public EventsCRUD() { db = new AppDbContext(); }
        public IEnumerable<TimeTableClient> GetEvents()
        {
            // получаем объекты из бд и выводим на консоль
            var TT = db.TimeTableClients.ToList();
            //Console.WriteLine("DT: " + TT.First().EventDateTime.ToString("yyyy-mm-dd"));
            return db.TimeTableClients.ToList();
        }

        public int DeletePlayerFromEvent(long DeletePlayerId, long DeleteEventId)
        {
            db.TimeTableClients.Remove(db.TimeTableClients.ToList().Find(x => (x.ClientId == DeletePlayerId) && (x.TimeTableId == DeleteEventId)));
            db.SaveChanges();

            return 0;
        }
        public int RegOnEvent(ReqRegistration req)
        {
            int i = 0;
            for (i = 0; (i < req.phone.Length) && (req.phone[i] != '9'); i++) ;
            req.phone = "+79" + req.phone.Remove(0, i+1);
            
            Client client = db.Clients.ToList().Find(c => c.phone == req.phone);
            if (client == null) {
                client = db.Clients.ToList().Find(c => c.Name == req.ClientName);
            }
            if ((client == null) && (req.email != null)) {
                client = db.Clients.ToList().Find(c => c.email == req.email);
            }
            if (client == null) {
                client = new Client { Name = req.ClientName, email = req.email, phone = req.phone, Role = "client", RegDate = DateTime.UtcNow };
                db.Clients.Add(client);
                db.SaveChanges();
                client = db.Clients.ToList().Find(c => c.phone == req.phone);
            }
            var ttc = new TimeTableClient { TimeTableId = req.TimeTableId, ClientId = client.Id, Comment = req.comment };
            try
            {
                db.TimeTableClients.Add(ttc);
                db.SaveChanges();
            }
            catch (Exception ex) { Console.WriteLine("Exception: " + ex.Message); return 1; }

            return 0;
        }

    }
}

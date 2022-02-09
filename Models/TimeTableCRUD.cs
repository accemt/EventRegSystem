using EventRegSystem.Models.Domain;
using Microsoft.EntityFrameworkCore;
//using System.Random

namespace EventRegSystem.Models
{
    public class TimeTableCRUD
    {
        private AppDbContext db;
        public TimeTableCRUD() { db = new AppDbContext(); }
        public IEnumerable<TimeTable> GetEvents()
        {
            var TT = db.TimeTables.Include(x => x.TimeTableClients).ThenInclude(xc => xc.Client).Include(x => x.HeadClient).ToList();

            return db.TimeTables.ToList();
        }

        public int AddEvent(TimeTable tt, string? HeadClientPhone)
        {
            if (HeadClientPhone != null) {
                tt.HeadClient = db.Clients.ToList().Find(c => c.phone == HeadClientPhone);
                if (tt.HeadClient == null)
                    return 1;
            }
            else
                return 1;
            
            Console.WriteLine("TT-Add: " + tt.Name + " | " + tt.MembersCount + " | " + tt.EventDateTime);
            db.TimeTables.Add(tt);
            db.SaveChanges();

            return 0;
        }

        public int AddEvent(int count = 1)
        {
            Random r = new Random();
            var tt = new TimeTable { EventDateTime = DateTime.UtcNow, Name = "Волейбол", MembersCount = r.Next(10, 16) };
            
            db.TimeTables.Add(tt);
            db.SaveChanges();

            return 0;
        }

        public int UpdateEvent(TimeTable tt, string? HeadClientPhone)
        {
            if (HeadClientPhone != null)
                tt.HeadClient = db.Clients.ToList().Find(c => c.phone == HeadClientPhone);
            

            Console.WriteLine("TT-Upd: " + tt.EventId + " | "+ tt.Name + " | " + tt.MembersCount + " | " + tt.EventDateTime);
            var ttUpd = db.TimeTables.ToList().Find(x => x.EventId == tt.EventId);
            ttUpd.Name = tt.Name; ttUpd.EventDateTime = tt.EventDateTime; ttUpd.MembersCount = tt.MembersCount;
            var client = db.Clients.Where(x => x.phone == tt.HeadClient.phone).First();
            if (client == null)
                return 1;
            ttUpd.HeadClient = client;

            db.SaveChanges();

            return 0;
        }

        public int DeleteEvent(long DeleteId)
        {
            db.TimeTables.Remove(db.TimeTables.ToList().Find(x => x.EventId == DeleteId));
            db.SaveChanges();

            return 0;
        }

    }
}

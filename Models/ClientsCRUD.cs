using EventRegSystem.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace EventRegSystem.Models
{
    public class ClientsCRUD
    {
        private AppDbContext db;
        public ClientsCRUD() { db = new AppDbContext(); }
        public IEnumerable<Client> GetClients()
        {
            // получаем объекты из бд и выводим на консоль
            var users = db.Clients.ToList();
            /*Console.WriteLine("Users list:");
            foreach (Client u in users)
            {
                Console.WriteLine($"{u.Id}.{u.Name} - {u.RegDate}");
            }*/
            return db.Clients.ToList();
        }

        public int AddClient(Client client)
        {
            if ((client.RegDate == null) || (client.RegDate == DateTime.Parse("01.01.0001 0:00:00")))
                client.RegDate = DateTime.UtcNow;
            client.RegDate = client.RegDate.ToUniversalTime().AddHours(TimeZoneInfo.Local.BaseUtcOffset.Hours);
            
            db.Clients.Add(client);
            db.SaveChanges();
            return 0;
        }
        public int AddUsers(int count = 2)
        {
            List<Client> users = new List<Client>();
            Client user1;
            var rand = new Random();
            // добавление данных
            for (int i = 0; i < count; i++) {
                // создаем два объекта User
                user1 = new Client { Name = "User" + rand.Next(1, 100), RegDate = System.DateTime.UtcNow, email = "test@test.com" };
                db.Clients.AddRange(user1);
                db.SaveChanges();
            }
            //User user2 = new User { Name = "Alice", RegDate = 26 };

            // добавляем их в бд
            
            return 0;
        }

    }
}

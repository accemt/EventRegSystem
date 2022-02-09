using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventRegSystem.Models.Domain
{
    [Table("TimeTable")]
    public class TimeTable
    {
        //public TimeTable() { Players = new List<Client>(); }

        [Key]
        public long EventId { get; set; }
        public string Name { get; set; } // имя пользователя
        public int MembersCount { get; set; }
        public Client? HeadClient { get; set; }
        public ICollection<TimeTableClient> TimeTableClients { get; set; }
        //public ICollection<Client> clients { get; set; }
        public DateTime EventDateTime{ get; set; } // дата регистрации
    }
}

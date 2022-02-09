using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventRegSystem.Models.Domain
{
    [Table("Clients")]
    public class Client
    {
        //public Client() { Events = new List<TimeTable>(); }
        [Key]
        public long Id { get; set; }
        public string Name { get; set; } // имя пользователя
        public string? email { get; set; }
        public string? phone { get; set; }
        public DateTime RegDate { get; set; } // дата регистрации
        public string? Role { get; set; }
        public ICollection<TimeTableClient> TimeTableClients { get; set; }
        //public ICollection<TimeTable> timeTables { get; set; }
    }
}

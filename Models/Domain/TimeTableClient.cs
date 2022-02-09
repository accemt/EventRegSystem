using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventRegSystem.Models.Domain
{
    public class TimeTableClient
    {
        //public Client() { Events = new List<TimeTable>(); }
        [Key]
        public long ClientId { get; set; }
        [Key]
        public long TimeTableId { get; set; }
        public Client Client { get; set;}
        public TimeTable TimeTableSpec { get; set; }
        public string? Comment { get; set; }

    }
}

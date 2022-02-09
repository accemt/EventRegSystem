namespace EventRegSystem.Models
{
    public class ReqRegistration
    {
        public ReqRegistration() { comment = ""; email = ""; }
        public long TimeTableId { get; set; }
        public string? ClientName { get; set; }
        public string? email { get; set; }
        public string? phone { get; set; }
        public string? comment { get; set; }
    }
}

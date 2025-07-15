namespace WebApplication1.Models.LogEntry
{
    public class ActivityLog
    {
        public string ShortDescription { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public string CreatedAt { get; set; } = string.Empty;
    }
}

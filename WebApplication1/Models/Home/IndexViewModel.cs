using WebApplication1.Models.LogEntry;

namespace WebApplication1.Models.Home
{
    public class IndexViewModel
    {
        public List<ActivityLog> RecentActivities { get; set; } = new();
    }
}

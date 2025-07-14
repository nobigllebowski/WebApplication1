using WebApplication1.Models.Log;

namespace WebApplication1.Models.Home
{
    public class IndexViewModel
    {
        public List<ActivityLog> RecentActivities { get; set; } = new();
    }
}

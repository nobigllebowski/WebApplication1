using WebApplication1.Models.Department;

namespace WebApplication1.Models.Analytics
{
    public class AnalyticsViewModel
    {
        public int TotalStudents { get; set; }
        public double AverageScore { get; set; }
        public List<DepartmentStatViewModel> DepartmentsStats { get; set; } = new List<DepartmentStatViewModel>();
        public Dictionary<string, int> ScoreDistribution { get; set; } 
    }
}

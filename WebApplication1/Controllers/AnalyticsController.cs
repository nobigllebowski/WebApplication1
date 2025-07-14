using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models.Analytics;
using WebApplication1.Models.Department;

namespace WebApplication1.Controllers
{
    public class AnalyticsController : Controller
    {
        private readonly IStudentService _studentService;
        private readonly IDepartmentService _departmentService;

        public AnalyticsController(IStudentService studentService, IDepartmentService departmentService)
        {
            _studentService = studentService;
            _departmentService = departmentService;
        }

        public async Task<IActionResult> Index()
        {
            var students = await _studentService.GetAllActiveStudentsAsync();
            var departments = await _departmentService.GetAllActiveDepartmentsAsync();
            var model = new AnalyticsViewModel
            {
                TotalStudents = students.Count(),
                AverageScore = students.Any() ? students.Average(s => s.Average) : 0,
                DepartmentsStats = departments
                   .Select(d => new DepartmentStatViewModel
                   {
                       Name = d.Name,
                       StudentCount = d.Students?.Count ?? 0, 
                       AvgScore = d.Students?.Any() == true ? d.Students.Average(s => s.Average) : 0 
                   }).ToList(),
                ScoreDistribution = await GetScoreDistribution()
            };

            return View(model);
        }

        private async Task<Dictionary<string, int>> GetScoreDistribution()
        {
            var students = await _studentService.GetAllStudentsAsync();
            return new Dictionary<string, int>
            {
                ["Отлично (5)"] = students.Count(s => s.Average >= 4.5),
                ["Хорошо (4)"] = students.Count(s => s.Average >= 3.5 && s.Average < 4.5),
                ["Удовлетворительно (3)"] = students.Count(s => s.Average >= 2.5 && s.Average < 3.5),
                ["Неудовлетворительно (2)"] = students.Count(s => s.Average < 2.5)
            };
        }
    }
}

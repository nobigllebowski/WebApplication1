using Application.DTOs.LogEntry;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Helpers;
using WebApplication1.Models.Home;
using WebApplication1.Models.Log;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILoggingService _loggingService;

        public HomeController(ILoggingService loggingService) 
        {
            _loggingService = loggingService;
        }

        public IActionResult Index()
        {
            var logs = _loggingService.GetLogs(new LogEntryFilterDto() { PageSize = 3 });
            var model = new IndexViewModel
            {
                RecentActivities = logs.Items
                .Select(l => new ActivityLog
                {
                    ShortDescription = l.ShortDescription,
                    CreatedAt = l.CreatedDate.ToRelativeTime()
                })
                .ToList(),
            };

            return View(model);
        }
    }
}

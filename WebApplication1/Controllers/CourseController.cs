using Application.DTOs.Course;
using Application.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models.Course;
using WebApplication1.Models.Pagination;

namespace WebApplication1.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourseService _courseService;
        private readonly IMapper _mapper;

        public CourseController(
            ICourseService courseService,
            IMapper mapper)
        {
            _courseService = courseService;
            _mapper = SetMapper();
        }

        // GET: Course
        public IActionResult Index(CourseFilterViewModel filter)
        {
            var filterDto = _mapper.Map<CourseFilterDto>(filter);

            var courses = _courseService.GetCourses(filterDto);

            var resultViewModel = new CourseListViewModel
            {
                Courses = _mapper.Map<List<CourseViewModel>>(courses.Items),
                Filter = filter,
                PaginationInfo = new PaginationInfoViewModel
                {
                    TotalItems = courses.TotalCount,
                    PageSize = courses.PageSize,
                    CurrentPage = courses.PageIndex
                }
            };

            return View(resultViewModel);
        }

        // GET: Course/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            var courseDto = await _courseService.GetCourseByIdAsync(id);
            if (courseDto == null)
            {
                return NotFound();
            }

            var courseModel = _mapper.Map<CourseViewModel>(courseDto);
            return View(courseModel);
        }

        // GET: Course/Create
        public IActionResult Create()
        {
            return View(new CreateCourseViewModel());
        }

        // POST: Course/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateCourseViewModel model)
        {
            if (ModelState.IsValid)
            {
                var modelDto = _mapper.Map<CreateCourseDto>(model);
                await _courseService.CreateCourseAsync(modelDto);
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        // POST: Course/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _courseService.DeactivateCourseAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private void AddModelErrors(IEnumerable<string> errors)
        {
            foreach (var error in errors)
            {
                ModelState.AddModelError(string.Empty, error);
            }
        }

        public IMapper SetMapper()
        {
            ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddConsole();
            });

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CourseFilterViewModel, CourseFilterDto>();
                cfg.CreateMap<CourseDto, CourseViewModel>();
                cfg.CreateMap<CreateCourseViewModel, CreateCourseDto>();
            }, loggerFactory);

            return config.CreateMapper();
        }
    }
}

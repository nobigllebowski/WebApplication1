using Application.DTOs.Department;
using Application.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models.Department;
using WebApplication1.Models.Pagination;

namespace WebApplication1.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentService;
        private readonly IMapper _mapper;

        public DepartmentController(
            IDepartmentService departmentService,
            IMapper mapper)
        {
            _departmentService = departmentService;
            _mapper = SetMapper();
        }

        // GET: Department
        public IActionResult Index(DepartmentFilterViewModel filter)
        {
            var filterDto = _mapper.Map<DepartmentFilterDto>(filter);

            var departments = _departmentService.GetDepartments(filterDto);

            var resultViewModel = new DepartmentListViewModel
            {
                Departments = _mapper.Map<List<DepartmentViewModel>>(departments.Items),
                Filter = filter,
                PaginationInfo = new PaginationInfoViewModel
                {
                    TotalItems = departments.TotalCount,
                    PageSize = departments.PageSize,
                    CurrentPage = departments.PageIndex
                }
            };

            return View(resultViewModel);
        }

        // GET: Department/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            var departmentDto = await _departmentService.GetDepartmentByIdAsync(id);
            if (departmentDto == null)
            {
                return NotFound();
            }

            var departmentModel = _mapper.Map<DepartmentViewModel>(departmentDto);
            return View(departmentModel);
        }

        // GET: Department/Create
        public IActionResult Create()
        {
            return View(new CreateDepartmentViewModel());
        }

        // POST: Department/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateDepartmentViewModel model)
        {
            if (ModelState.IsValid)
            {
                var modelDto = _mapper.Map<CreateDepartmentDto>(model);
                await _departmentService.CreateDepartmentAsync(modelDto);
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        // POST: Department/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _departmentService.DeactivateDepartmentAsync(id);
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
                cfg.CreateMap<DepartmentFilterViewModel, DepartmentFilterDto>();
                cfg.CreateMap<DepartmentDto, DepartmentViewModel>();
                cfg.CreateMap<CreateDepartmentViewModel, CreateDepartmentDto>();
            }, loggerFactory);

            return config.CreateMapper();
        }
    }
}

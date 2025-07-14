using Application.DTOs.Teacher;
using Application.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication1.Models.Pagination;
using WebApplication1.Models.Teacher;

namespace WebApplication1.Controllers
{
    public class TeacherController : Controller
    {
        private readonly ITeacherService _teacherService;
        private readonly IDepartmentService _departmentService;
        private readonly IMapper _mapper;

        public TeacherController(
            ITeacherService teacherService,
            IDepartmentService departmentService,
            IMapper mapper)
        {
            _teacherService = teacherService;
            _departmentService = departmentService;
            _mapper = SetMapper();
        }

        // GET: Teacher
        public async Task<IActionResult> Index(TeacherFilterViewModel filter)
        {
            var filterDto = _mapper.Map<TeacherFilterDto>(filter);

            var teachers = await _teacherService.GetTeachersAsync(filterDto);

            var resultViewModel = new TeacherListViewModel
            {
                Teachers = _mapper.Map<List<TeacherViewModel>>(teachers.Items),
                Filter = filter,
                PaginationInfo = new PaginationInfoViewModel
                {
                    TotalItems = teachers.TotalCount,
                    PageSize = teachers.PageSize,
                    CurrentPage = teachers.PageIndex
                }
            };

            return View(resultViewModel);
        }

        // GET: Teacher/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            var teacherDto = await _teacherService.GetTeacherByIdAsync(id);
            if (teacherDto == null)
            {
                return NotFound();
            }

            var teacherModel = _mapper.Map<TeacherViewModel>(teacherDto);
            return View(teacherModel);
        }

        // GET: Teacher/Create
        public async Task<IActionResult> Create()
        {
            var departments = await _departmentService.GetAllDepartmentsAsync();
            var model = new CreateTeacherViewModel
            {
                // Инициализация даты по умолчанию
                DateOfBirth = DateTime.Now.AddYears(-18),

                Departments = departments
                    .Select(d => new SelectListItem
                    {
                        Value = d.Id.ToString(),
                        Text = d.Name
                    })
                .ToList()
            };

            return View(model);
        }

        // POST: Teacher/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateTeacherViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var modelDto = _mapper.Map<CreateTeacherDto>(model);
                await _teacherService.CreateTeacherAsync(modelDto);
                return RedirectToAction(nameof(Index));
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError("DateOfBirth", ex.Message);
                return View(model);
            }
        }

        // GET: Teacher/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            var teacherDto = await _teacherService.GetTeacherByIdAsync(id);

            if (teacherDto == null)
            {
                return NotFound();
            }

            var teacher = _mapper.Map<EditTeacherViewModel>(teacherDto);

            return View(teacher);
        }

        // POST: Teacher/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, EditTeacherViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var modelDto = _mapper.Map<UpdateTeacherDto>(model);
                await _teacherService.UpdateTeacherAsync(modelDto);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // POST: Teacher/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _teacherService.DeactivateTeacherAsync(id);
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
                cfg.CreateMap<TeacherFilterViewModel, TeacherFilterDto>();
                cfg.CreateMap<TeacherDto, TeacherViewModel>();
                cfg.CreateMap<CreateTeacherViewModel, CreateTeacherDto>()
                    .ForMember(dest => dest.DepartmentId, opt => opt.MapFrom(src => src.SelectedDepartmentId));
                cfg.CreateMap<TeacherDto, EditTeacherViewModel>();
                cfg.CreateMap<EditTeacherViewModel, UpdateTeacherDto>();
            }, loggerFactory);

            return config.CreateMapper();
        }
    }
}

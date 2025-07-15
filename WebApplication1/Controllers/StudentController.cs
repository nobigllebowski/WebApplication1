using Application.DTOs.Student;
using Application.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication1.Models.Pagination;
using WebApplication1.Models.Student;

namespace WebApplication1.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;
        private readonly IDepartmentService _departmentService;
        private readonly IMapper _mapper;

        public StudentController(
            IStudentService studentService,
            IDepartmentService departmentService,
            IMapper mapper)
        {
            _studentService = studentService;
            _departmentService = departmentService;
            _mapper = SetMapper();
        }        

        // GET: Student
        public async Task<IActionResult> Index(StudentFilterViewModel filter)
        {            
            var filterDto = _mapper.Map<StudentFilterDto>(filter);

            var students = await _studentService.GetStudentsAsync(filterDto);

            var resultViewModel = new StudentListViewModel
            {
                Students = _mapper.Map<List<StudentViewModel>>(students.Items),
                Filter = filter,
                PaginationInfo = new PaginationInfoViewModel
                {
                    TotalItems = students.TotalCount,
                    PageSize = students.PageSize,
                    CurrentPage = students.PageIndex
                }
            };

            return View(resultViewModel);
        }

        // GET: Student/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            var studentDto = await _studentService.GetStudentByIdAsync(id);
            if (studentDto == null)
            {
                return NotFound();
            }
            
            var studentModel = _mapper.Map<StudentViewModel>(studentDto);
            return View(studentModel);
        }

        // GET: Student/Create
        public async Task<IActionResult> Create()
        {
            var departments = await _departmentService.GetAllActiveDepartmentsAsync();
            var model = new CreateStudentViewModel
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

        // POST: Student/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateStudentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model); 
            }

            try
            {
                var modelDto = _mapper.Map<CreateStudentDto>(model);
                await _studentService.CreateStudentAsync(modelDto);
                return RedirectToAction(nameof(Index));
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError("DateOfBirth", ex.Message);
                return View(model);
            }
        }

        // GET: Student/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            var studentDto = await _studentService.GetStudentByIdAsync(id);

            if (studentDto == null)
            {
                return NotFound();
            }

            var student = _mapper.Map<EditStudentViewModel>(studentDto);
            
            return View(student);
        }

        // POST: Student/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, EditStudentViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var modelDto = _mapper.Map<UpdateStudentDto>(model);
                await _studentService.UpdateStudentAsync(modelDto);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // POST: Student/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _studentService.DeactivateStudentAsync(id);
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
                cfg.CreateMap<StudentFilterViewModel, StudentFilterDto>();
                cfg.CreateMap<StudentDto, StudentViewModel>();
                cfg.CreateMap<CreateStudentViewModel, CreateStudentDto>()
                    .ForMember(dest => dest.DepartmentId, opt => opt.MapFrom(src => src.SelectedDepartmentId));
                cfg.CreateMap<StudentDto, EditStudentViewModel>();
                cfg.CreateMap<EditStudentViewModel, UpdateStudentDto>();
            }, loggerFactory);

            return config.CreateMapper();
        }
    }
}

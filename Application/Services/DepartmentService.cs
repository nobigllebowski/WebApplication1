using Application.Common.LogTemplates;
using Application.DTOs.Department;
using Application.DTOs.LogEntry;
using Application.Extensions;
using Application.Interfaces;
using AutoMapper;
using Common.Pagination;
using Domain.Entities;
using Infrastructure.Persistence.Repositories.Base;

namespace Application.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepo;
        private readonly IStudentRepository _studentRepo;
        private readonly ITeacherRepository _teacherRepo;
        private readonly ILoggingService _loggingService;
        private readonly IMapper _mapper;

        public DepartmentService(
            IDepartmentRepository departmentRepo,
            IStudentRepository studentRepo,
            ITeacherRepository teacherRepo,
            ILoggingService loggingService,
            IMapper mapper)
        {
            _departmentRepo = departmentRepo;
            _studentRepo = studentRepo;
            _teacherRepo = teacherRepo;
            _loggingService = loggingService;
            _mapper = mapper;
        }

        public async Task<DepartmentDto> GetDepartmentByIdAsync(Guid id)
        {
            var Department = await _departmentRepo.GetByIdAsync(id);
            return _mapper.Map<DepartmentDto>(Department);
        }

        public PaginatedList<DepartmentDto> GetDepartments(DepartmentFilterDto filter)
        {
            var departments = _departmentRepo.GetFilteredDepartments(
                nameFilter: filter.NameFilter,
                isActive: filter.IsActive,
                pageNumber: filter.Page,
                pageSize: filter.PageSize);

            var departmentDtos = _mapper.Map<List<DepartmentDto>>(departments.Items);

            return new PaginatedList<DepartmentDto>(
                departmentDtos,
                departments.TotalCount,
                departments.PageIndex,
                departments.PageSize);
        }

        public async Task<List<DepartmentDto>> GetAllDepartmentsAsync()
        {
            var departments = await _departmentRepo.GetAllAsync();
            return _mapper.Map<List<DepartmentDto>>(departments);
        }

        public async Task<List<DepartmentDto>> GetAllActiveDepartmentsAsync()
        {
            var departments = await _departmentRepo.GetAllActiveAsync();
            return _mapper.Map<List<DepartmentDto>>(departments);
        }

        public async Task CreateDepartmentAsync(CreateDepartmentDto dto)
        {
            ArgumentNullException.ThrowIfNull(dto);

            var department = new Department(dto.Name);

            await _departmentRepo.AddAsync(department);
            await _departmentRepo.SaveChangesAsync();

            await _loggingService.AddLogAsync(
                new CreateLogEntryDto()
                {
                    EntityId = department.Id.ToString(),
                    EntityName = "Department",
                    ShortDescription = LogDescriptions.GetDescription(
                                        LogActionType.Create,
                                        LogEntityType.Department,
                                        $"{department.Name}")
                });
        }

        public async Task UpdateDepartmentAsync(UpdateDepartmentDto dto)
        {
            dto.ThrowIfNull(nameof(dto));
            dto.Id.ThrowIfNull(nameof(dto.Id));

            var department = await _departmentRepo.GetByIdAsync(dto.Id);
            department.ThrowIfNull(nameof(department));

            department.SetName(dto.Name);
            department.UpdateModifiedDate();

            _departmentRepo.Update(department);
            await _departmentRepo.SaveChangesAsync();
        }

        public async Task DeactivateDepartmentAsync(Guid id)
        {
            var department = await _departmentRepo.GetByIdAsync(id);
            department.ThrowIfNull(nameof(department));

            _departmentRepo.Delete(department);
            await _departmentRepo.SaveChangesAsync();

            var students = await _studentRepo.GetAllActiveAsync();
            var departmentStudents = students.Where(_ => _.DepartmentId == department.Id);
            foreach (var student in departmentStudents)
            {
                _studentRepo.Delete(student);
            }

            await _studentRepo.SaveChangesAsync();

            var teachers = await _teacherRepo.GetAllActiveAsync();
            var teacherStudents = teachers.Where(_ => _.DepartmentId == department.Id);
            foreach (var teacher in teacherStudents)
            {
                _teacherRepo.Delete(teacher);
            }

            await _teacherRepo.SaveChangesAsync();
        }
    }
}

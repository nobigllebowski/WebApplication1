using Application.Common.LogTemplates;
using Application.DTOs.LogEntry;
using Application.DTOs.Student;
using Application.Extensions;
using Application.Interfaces;
using AutoMapper;
using Common.Pagination;
using Domain.Entities;
using Infrastructure.Persistence.Repositories.Base;

namespace Application.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepo;
        private readonly IDepartmentRepository _departmentRepo;
        private readonly ILoggingService _loggingService;
        private readonly IMapper _mapper;

        public StudentService(
            IStudentRepository studentRepo,
            IDepartmentRepository departmentRepo,
            ILoggingService loggingService,
            IMapper mapper)
        {
            _studentRepo = studentRepo;
            _departmentRepo = departmentRepo;
            _loggingService = loggingService;
            _mapper = mapper;
        }

        public async Task<StudentDto> GetStudentByIdAsync(Guid id)
        {
            var student = await _studentRepo.GetByIdAsync(id);
            return _mapper.Map<StudentDto>(student);
        }

        public async Task<PaginatedList<StudentDto>> GetStudentsAsync(StudentFilterDto filter)
        {
            var students = await _studentRepo.GetFilteredStudentsAsync(
                nameFilter: filter.NameFilter,
                minAge: filter.MinAge,
                maxAge: filter.MaxAge,
                departmentId: filter.DepartmentId,
                isActive: filter.IsActive,
                pageNumber: filter.Page,
                pageSize: filter.PageSize);

            var studentDtos = _mapper.Map<List<StudentDto>>(students.Items);
            
            var departmentIds = studentDtos
                .Select(s => s.DepartmentId)
                .Distinct()
                .ToList();

            var departments = await _departmentRepo.GetAllAsync();
            var departmentDicts = departments
                .Where(d => departmentIds.Contains(d.Id))
                .ToDictionary(d => d.Id, d => d.Name);

            foreach (var student in studentDtos)
            {
                student.DepartmentName = departmentDicts.GetValueOrDefault(student.DepartmentId) ?? string.Empty;
            }

            return new PaginatedList<StudentDto>(
                studentDtos,
                students.TotalCount,
                students.PageIndex,
                students.PageSize);
        }

        public async Task<List<StudentDto>> GetAllStudentsAsync()
        {
            var students = await _studentRepo.GetAllAsync();
            return _mapper.Map<List<StudentDto>>(students);
        }

        public async Task<List<StudentDto>> GetAllActiveStudentsAsync()
        {
            var students = await _studentRepo.GetAllActiveAsync();
            return _mapper.Map<List<StudentDto>>(students);
        }

        public async Task CreateStudentAsync(CreateStudentDto dto)
        {
            ArgumentNullException.ThrowIfNull(dto);

            var department = await _departmentRepo.GetByIdAsync(dto.DepartmentId);
            ArgumentNullException.ThrowIfNull(department, $"Department with ID {dto.DepartmentId} not found");

            var student = new Student(
                dto.FirstName,
                dto.LastName);

            student.SetDateOfBirth(dto.DateOfBirth);
            student.SetDepartment(department);

            await _studentRepo.AddAsync(student);
            await _studentRepo.SaveChangesAsync();

            await _loggingService.AddLogAsync(
                new CreateLogEntryDto()
                    {
                        EntityId = student.Id.ToString(),
                        EntityName = "Student",
                        ShortDescription = LogDescriptions.GetDescription(
                                        LogActionType.Create,
                                        LogEntityType.Student,
                                        $"{student.LastName} {student.FirstName}")
                    }
                );
        }

        public async Task UpdateStudentAsync(UpdateStudentDto dto)
        {
            dto.ThrowIfNull(nameof(dto));
            dto.Id.ThrowIfNull(nameof(dto.Id));

            var student = await _studentRepo.GetByIdAsync(dto.Id);
            student.ThrowIfNull(nameof(student));

            var department = await _departmentRepo.GetByIdAsync(dto.DepartmentId);
            department.ThrowIfNull(nameof(department));

            student.SetDepartment(department);
            student.SetFirstName(dto.FirstName);
            student.SetLastName(dto.LastName);
            student.SetDateOfBirth(dto.DateOfBirth);
            student.UpdateModifiedDate();

            _studentRepo.Update(student);
            await _studentRepo.SaveChangesAsync();

            await _loggingService.AddLogAsync(
                new CreateLogEntryDto()
                {
                    EntityId = student.Id.ToString(),
                    EntityName = "Student",
                    ShortDescription = LogDescriptions.GetDescription(
                                        LogActionType.Update,
                                        LogEntityType.Student,
                                        $"{student.LastName} {student.FirstName}")
                    }
                );
        }

        public async Task DeactivateStudentAsync(Guid id)
        {
            var student = await _studentRepo.GetByIdAsync(id);
            student.ThrowIfNull(nameof(student));

            _studentRepo.Delete(student);
            await _studentRepo.SaveChangesAsync();
        }
    }
}

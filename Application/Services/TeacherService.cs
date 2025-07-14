using Application.DTOs.Student;
using Application.DTOs.Teacher;
using Application.Extensions;
using Application.Interfaces;
using AutoMapper;
using Common.Pagination;
using Domain.Entities;
using Infrastructure.Persistence.Repositories.Base;
using System.Linq;

namespace Application.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly ITeacherRepository _teacherRepo;
        private readonly IDepartmentRepository _departmentRepo;
        private readonly IMapper _mapper;

        public TeacherService(
            ITeacherRepository teacherRepo,
            IDepartmentRepository departmentRepo,
            IMapper mapper)
        {
            _teacherRepo = teacherRepo;
            _departmentRepo = departmentRepo;
            _mapper = mapper;
        }

        public async Task<TeacherDto> GetTeacherByIdAsync(Guid id)
        {
            var teacher = await _teacherRepo.GetByIdAsync(id);
            return _mapper.Map<TeacherDto>(teacher);
        }

        public async Task<PaginatedList<TeacherDto>> GetTeachersAsync(TeacherFilterDto filter)
        {
            var teachers = await _teacherRepo.GetFilteredTeachersAsync(
                nameFilter: filter.NameFilter,
                minAge: filter.MinAge,
                maxAge: filter.MaxAge,
                departmentId: filter.DepartmentId,
                isActive: filter.IsActive,
                pageNumber: filter.Page,
                pageSize: filter.PageSize);

            var teacherDtos = _mapper.Map<List<TeacherDto>>(teachers.Items);

            var departmentIds = teacherDtos
                .Select(s => s.DepartmentId)
                .Distinct()
                .ToList();

            var departments = await _departmentRepo.GetAllAsync();
            var departmentDicts = departments
                .Where(d => departmentIds.Contains(d.Id))
                .ToDictionary(d => d.Id, d => d.Name);

            foreach (var teacher in teacherDtos)
            {
                teacher.DepartmentName = departmentDicts.GetValueOrDefault(teacher.DepartmentId) ?? string.Empty;
            }

            return new PaginatedList<TeacherDto>(
                teacherDtos,
                teachers.TotalCount,
                teachers.PageIndex,
                teachers.PageSize);
        }

        public async Task<List<TeacherDto>> GetAllTeachersAsync()
        {
            var teachers = await _teacherRepo.GetAllAsync();
            return _mapper.Map<List<TeacherDto>>(teachers);
        }

        public async Task<List<TeacherDto>> GetAllActiveTeachersAsync()
        {
            var teachers = await _teacherRepo.GetAllActiveAsync();
            return _mapper.Map<List<TeacherDto>>(teachers);
        }

        public async Task CreateTeacherAsync(CreateTeacherDto dto)
        {
            ArgumentNullException.ThrowIfNull(dto);

            var department = await _departmentRepo.GetByIdAsync(dto.DepartmentId);
            ArgumentNullException.ThrowIfNull(department, $"Department with ID {dto.DepartmentId} not found");

            var teacher = new Teacher(
                dto.FirstName,
                dto.LastName);

            teacher.SetDateOfBirth(dto.DateOfBirth);
            teacher.SetDepartment(department);

            await _teacherRepo.AddAsync(teacher);
            await _teacherRepo.SaveChangesAsync();
        }

        public async Task UpdateTeacherAsync(UpdateTeacherDto dto)
        {
            dto.ThrowIfNull(nameof(dto));
            dto.Id.ThrowIfNull(nameof(dto.Id));

            var teacher = await _teacherRepo.GetByIdAsync(dto.Id);
            teacher.ThrowIfNull(nameof(teacher));

            var department = await _departmentRepo.GetByIdAsync(dto.DepartmentId);
            department.ThrowIfNull(nameof(department));

            teacher.SetDepartment(department);
            teacher.SetFirstName(dto.FirstName);
            teacher.SetLastName(dto.LastName);
            teacher.SetDateOfBirth(dto.DateOfBirth);
            teacher.UpdateModifiedDate();

            _teacherRepo.Update(teacher);
            await _teacherRepo.SaveChangesAsync();
        }

        public async Task DeactivateTeacherAsync(Guid id)
        {
            var teacher = await _teacherRepo.GetByIdAsync(id);
            teacher.ThrowIfNull(nameof(teacher));

            _teacherRepo.Delete(teacher);
            await _teacherRepo.SaveChangesAsync();
        }
    }
}

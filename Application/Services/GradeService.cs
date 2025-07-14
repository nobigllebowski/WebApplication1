using Application.DTOs.Grade;
using Application.Extensions;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Persistence.Repositories.Base;

namespace Application.Services
{
    public class GradeService : IGradeService
    {
        private readonly IStudentRepository _studentRepo;
        private readonly IGradeRepository _gradeRepo;
        private readonly ICourseRepository _courseRepo;
        private readonly ITeacherRepository _teacherRepo;
        private readonly IMapper _mapper;

        public GradeService(
            IStudentRepository studentRepo,
            IGradeRepository gradeRepo,
            ICourseRepository courseRepo,
            ITeacherRepository teacherRepo,
            IMapper mapper)
        {
            _studentRepo = studentRepo;
            _gradeRepo = gradeRepo;
            _courseRepo = courseRepo;
            _teacherRepo = teacherRepo;
            _mapper = mapper;
        }

        public async Task<GradeDto> GetGradeByIdAsync(Guid id)
        {
            var grade = await _gradeRepo.GetByIdAsync(id);
            return _mapper.Map<GradeDto>(grade);
        }
        public async Task<List<GradeDto>> GetAllGradesAsync()
        {
            var grades = await _gradeRepo.GetAllAsync();
            return _mapper.Map<List<GradeDto>>(grades);
        }

        public async Task CreateGradeAsync(CreateGradeDto dto)
        {
            ArgumentNullException.ThrowIfNull(dto);

            var student = await _studentRepo.GetByIdAsync(dto.StudentId);
            ArgumentNullException.ThrowIfNull(student, $"Student with ID {dto.StudentId} not found");

            var course = await _courseRepo.GetByIdAsync(dto.CourseId);
            ArgumentNullException.ThrowIfNull(course, $"Course with ID {dto.CourseId} not found");

            var teacher = await _teacherRepo.GetByIdAsync(dto.TeacherId);
            ArgumentNullException.ThrowIfNull(teacher, $"Teacher with ID {dto.TeacherId} not found");

            var grade = new Grade(
                dto.Value,
                dto.Comment);

            grade.SetStudent(student);
            grade.SetCource(course);
            grade.SetTeacher(teacher);

            await _gradeRepo.AddAsync(grade);
            await _gradeRepo.SaveChangesAsync();
        }

        public async Task UpdateGradeAsync(UpdateGradeDto dto)
        {
            dto.ThrowIfNull(nameof(dto));
            dto.Id.ThrowIfNull(nameof(dto.Id));

            var grade = await _gradeRepo.GetByIdAsync(dto.Id);
            grade.ThrowIfNull(nameof(grade));

            var student = await _studentRepo.GetByIdAsync(dto.Id);
            student.ThrowIfNull(nameof(student));

            var course = await _courseRepo.GetByIdAsync(dto.CourseId);
            course.ThrowIfNull($"Course with ID {dto.CourseId} not found");

            var teacher = await _teacherRepo.GetByIdAsync(dto.TeacherId);
            teacher.ThrowIfNull($"Teacher with ID {dto.TeacherId} not found");

            grade.SetStudent(student);
            grade.SetCource(course);
            grade.SetTeacher(teacher);
            grade.SetComment(dto.Comment);
            grade.SetValue(dto.Value);
            grade.UpdateModifiedDate();

            _gradeRepo.Update(grade);
            await _gradeRepo.SaveChangesAsync();
        }

        public async Task DeactivateGradeAsync(Guid id)
        {
            var grade = await _gradeRepo.GetByIdAsync(id);
            grade.ThrowIfNull(nameof(grade));

            _gradeRepo.Delete(grade);
            await _gradeRepo.SaveChangesAsync();
        }
    }
}

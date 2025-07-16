using Application.DTOs.Course;
using Application.Extensions;
using Application.Interfaces;
using AutoMapper;
using Common.Pagination;
using Domain.Entities;
using Infrastructure.Persistence.Repositories.Base;

namespace Application.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepo;
        private readonly IMapper _mapper;

        public CourseService(
            ICourseRepository courseRepo,
            IMapper mapper)
        {
            _courseRepo = courseRepo;
            _mapper = mapper;
        }

        public async Task<CourseDto> GetCourseByIdAsync(Guid id)
        {
            var course = await _courseRepo.GetByIdAsync(id);
            return _mapper.Map<CourseDto>(course);
        }

        public PaginatedList<CourseDto> GetCourses(CourseFilterDto filter)
        {
            var courses = _courseRepo.GetFilteredCourses(
                nameFilter: filter.NameFilter,
                isActive: filter.IsActive,
                pageNumber: filter.Page,
                pageSize: filter.PageSize);

            var courseDtos = _mapper.Map<List<CourseDto>>(courses.Items);

            return new PaginatedList<CourseDto>(
                courseDtos,
                courses.TotalCount,
                courses.PageIndex,
                courses.PageSize);
        }

        public async Task<List<CourseDto>> GetAllCoursesAsync()
        {
            var courses = await _courseRepo.GetAllAsync();
            return _mapper.Map<List<CourseDto>>(courses);
        }

        public async Task<List<CourseDto>> GetAllActiveCoursesAsync()
        {
            var courses = await _courseRepo.GetAllActiveAsync();
            return _mapper.Map<List<CourseDto>>(courses);
        }

        public async Task CreateCourseAsync(CreateCourseDto dto)
        {
            ArgumentNullException.ThrowIfNull(dto);

            var course = new Course(
                dto.Name);

            await _courseRepo.AddAsync(course);
            await _courseRepo.SaveChangesAsync();
        }

        public async Task UpdateCourseAsync(UpdateCourseDto dto)
        {
            dto.ThrowIfNull(nameof(dto));
            dto.Id.ThrowIfNull(nameof(dto.Id));

            var course = await _courseRepo.GetByIdAsync(dto.Id);
            course.ThrowIfNull(nameof(course));

            course.SetName(dto.Name);
            course.UpdateModifiedDate();

            _courseRepo.Update(course);
            await _courseRepo.SaveChangesAsync();
        }

        public async Task DeactivateCourseAsync(Guid id)
        {
            var course = await _courseRepo.GetByIdAsync(id);
            course.ThrowIfNull(nameof(course));

            _courseRepo.Delete(course);
            await _courseRepo.SaveChangesAsync();
        }
    }
}

using Application.DTOs.Course;
using Common.Pagination;

namespace Application.Interfaces
{
    public interface ICourseService 
    {
        Task<CourseDto> GetCourseByIdAsync(Guid id);
        PaginatedList<CourseDto> GetCourses(CourseFilterDto filter);
        Task<List<CourseDto>> GetAllCoursesAsync();

        Task CreateCourseAsync(CreateCourseDto dto);
        Task UpdateCourseAsync(UpdateCourseDto dto);
        Task DeactivateCourseAsync(Guid id);
    }
}

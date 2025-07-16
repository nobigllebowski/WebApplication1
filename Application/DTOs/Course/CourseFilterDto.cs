using Common.Filters;

namespace Application.DTOs.Course
{
    public class CourseFilterDto : PaginationFilter
    {
        public string? NameFilter { get; set; }
        public bool IsActive { get;set; }
    }
}

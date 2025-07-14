using Common.Filters;

namespace Application.DTOs.Department
{
    public class DepartmentFilterDto : PaginationFilter
    {
        public string? NameFilter { get; set; }
        public bool IsActive { get; set; } = true;
    }
}

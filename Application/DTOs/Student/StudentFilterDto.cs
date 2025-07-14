using Common.Filters;

namespace Application.DTOs.Student
{
    public class StudentFilterDto : PaginationFilter
    {        
        public string? NameFilter { get; set; }
        public int? MinAge { get; set; } = 16;
        public int? MaxAge { get; set; } = 100;
        public Guid? DepartmentId { get; set; }
        public bool IsActive { get; set; } = true;
    }
}

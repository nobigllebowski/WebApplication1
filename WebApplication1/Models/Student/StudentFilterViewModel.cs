using WebApplication1.Models.Pagination;

namespace WebApplication1.Models.Student
{
    public class StudentFilterViewModel : PaginationFilterViewModel
    {
        public string NameFilter { get; set; }
        public int? MinAge { get; set; }
        public int? MaxAge { get; set; }
        public Guid? DepartmentId { get; set; }
    }
}

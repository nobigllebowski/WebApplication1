using WebApplication1.Models.Pagination;

namespace WebApplication1.Models.Department
{
    public class DepartmentFilterViewModel : PaginationFilterViewModel
    {
        public string NameFilter { get; set; }
    }
}

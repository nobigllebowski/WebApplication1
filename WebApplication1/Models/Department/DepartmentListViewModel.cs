using WebApplication1.Models.Pagination;

namespace WebApplication1.Models.Department
{
    public class DepartmentListViewModel
    {
        public List<DepartmentViewModel> Departments { get; set; }
        public DepartmentFilterViewModel Filter { get; set; }
        public PaginationInfoViewModel PaginationInfo { get; set; }
    }
}

using WebApplication1.Models.Pagination;

namespace WebApplication1.Models.Student
{
    public class StudentListViewModel
    {
        public List<StudentViewModel> Students { get; set; }
        public StudentFilterViewModel Filter { get; set; }
        public PaginationInfoViewModel PaginationInfo { get; set; }
    }
}

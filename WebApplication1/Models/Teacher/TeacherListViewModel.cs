using WebApplication1.Models.Pagination;

namespace WebApplication1.Models.Teacher
{
    public class TeacherListViewModel
    {
        public List<TeacherViewModel> Teachers { get; set; }
        public TeacherFilterViewModel Filter { get; set; }
        public PaginationInfoViewModel PaginationInfo { get; set; }
    }
}

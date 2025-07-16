using WebApplication1.Models.Pagination;

namespace WebApplication1.Models.Course
{
    public class CourseListViewModel
    {
        public List<CourseViewModel> Courses { get; set; }
        public CourseFilterViewModel Filter { get; set; }
        public PaginationInfoViewModel PaginationInfo { get; set; }
    }
}

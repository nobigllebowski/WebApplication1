using WebApplication1.Models.Pagination;

namespace WebApplication1.Models.Course
{
    public class CourseFilterViewModel : PaginationFilterViewModel
    {
        public string NameFilter { get; set; }
    }
}

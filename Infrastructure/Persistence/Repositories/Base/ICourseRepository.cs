using Common.Pagination;
using Domain.Entities;

namespace Infrastructure.Persistence.Repositories.Base
{
    public interface ICourseRepository : IBaseRepository<Course>
    {
        PaginatedList<Course> GetFilteredCourses(
            string? nameFilter = null,
            bool isActive = true,
            int pageNumber = 1,
            int pageSize = 10);
    }
}

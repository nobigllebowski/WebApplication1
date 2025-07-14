using Common.Pagination;
using Domain.Entities;

namespace Infrastructure.Persistence.Repositories.Base
{
    public interface IDepartmentRepository : IBaseRepository<Department>
    {
        PaginatedList<Department> GetFilteredDepartments(
            string? nameFilter = null,
            bool isActive = true,
            int pageNumber = 1,
            int pageSize = 10);
    }
}

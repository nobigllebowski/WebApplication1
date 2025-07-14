using Common.Pagination;
using Domain.Entities;

namespace Infrastructure.Persistence.Repositories.Base
{
    public interface ITeacherRepository : IBaseRepository<Teacher>
    {
        Task<PaginatedList<Teacher>> GetFilteredTeachersAsync(
            string? nameFilter = null,
            int? minAge = null,
            int? maxAge = null,
            Guid? departmentId = null,
            bool isActive = true,
            int pageNumber = 1,
            int pageSize = 10);
    }
}

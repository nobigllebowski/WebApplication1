using Common.Pagination;
using Domain.Entities;

namespace Infrastructure.Persistence.Repositories.Base
{
    public interface ILoggingRepository : IBaseRepository<LogEntry>
    {
        PaginatedList<LogEntry> GetFilteredLogs(
            int pageNumber = 1,
            int pageSize = 10);
    }
}

using Common.Pagination;
using Domain.Entities;
using Infrastructure.Persistence.Repositories.Base;

namespace Infrastructure.Persistence.Repositories.Entities
{
    public class LoggingRepository(AppDbContext context) : BaseRepository<LogEntry>(context), ILoggingRepository
    {
        public PaginatedList<LogEntry> GetFilteredLogs(
            int pageNumber = 1,
            int pageSize = 3)
        {
            var query = _context.Logs.AsQueryable();

            query = query.OrderByDescending(s => s.CreatedDate);

            return PaginatedList<LogEntry>.Create(query, pageNumber, pageSize);
        }
    }
}

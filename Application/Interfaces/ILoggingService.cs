using Application.DTOs.LogEntry;
using Common.Pagination;

namespace Application.Interfaces
{
    public interface ILoggingService
    {
        Task<LogEntryDto> GetLogByIdAsync(Guid id);
        PaginatedList<LogEntryDto> GetLogs(LogEntryFilterDto filter);
        Task<List<LogEntryDto>> GetAllLogsAsync();

        Task AddLogAsync(CreateLogEntryDto dto);
        Task DeactivateLogAsync(Guid id);
    }
}

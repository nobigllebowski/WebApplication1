using Application.DTOs.LogEntry;
using Application.Extensions;
using Application.Interfaces;
using AutoMapper;
using Common.Pagination;
using Domain.Entities;
using Infrastructure.Persistence.Repositories.Base;

namespace Application.Services
{
    public class LoggingService : ILoggingService
    {
        private readonly ILoggingRepository _loggingRepository;
        private readonly IMapper _mapper;

        public LoggingService(ILoggingRepository loggingRepository, IMapper mapper) 
        {
            _loggingRepository = loggingRepository;
            _mapper = mapper;
        }

        public async Task<LogEntryDto> GetLogByIdAsync(Guid id)
        {
            var log = await _loggingRepository.GetByIdAsync(id);
            return _mapper.Map<LogEntryDto>(log);
        }

        public PaginatedList<LogEntryDto> GetLogs(LogEntryFilterDto filter)
        {
            var logs = _loggingRepository.GetFilteredLogs(
                pageNumber: filter.Page,
                pageSize: filter.PageSize);

            var logDtos = _mapper.Map<List<LogEntryDto>>(logs.Items);
            return new PaginatedList<LogEntryDto>(
                logDtos,
                logs.TotalCount,
                logs.PageIndex,
                logs.PageSize);
        }

        public async Task<List<LogEntryDto>> GetAllLogsAsync()
        {
            var logs = await _loggingRepository.GetAllAsync();
            return _mapper.Map<List<LogEntryDto>>(logs);
        }

        public async Task AddLogAsync(CreateLogEntryDto dto)
        {
            dto.ThrowIfNull(nameof(dto));

            var log = new LogEntry()
            {
                EntityId = dto.EntityId,
                EntityName = dto.EntityName ?? "",
                ShortDescription = dto.ShortDescription,
                Description = dto.Description
            };

            await _loggingRepository.AddAsync(log);
            await _loggingRepository.SaveChangesAsync();
        }

        public async Task DeactivateLogAsync(Guid id)
        {
            var log = await _loggingRepository.GetByIdAsync(id);
            log.ThrowIfNull(nameof(log));

            log.Delete();

            _loggingRepository.Update(log);
            await _loggingRepository.SaveChangesAsync();
        }
    }
}

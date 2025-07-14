using Application.DTOs.LogEntry;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class LoggingProfile : Profile
    {
        public LoggingProfile() 
        {
            CreateMap<LogEntry, LogEntryDto>();
        }
    }
}

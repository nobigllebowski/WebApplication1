namespace Application.DTOs.LogEntry
{
    public class LogEntryDto
    {
        public Guid Id { get; set; }

        public string? EntityName { get; set; }
        public string? EntityId { get; set; }

        public string ShortDescription { get; set; }

        public string? Description { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}

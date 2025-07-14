namespace Application.DTOs.LogEntry
{
    public class CreateLogEntryDto
    {
        public string? EntityName { get; set; }
        public string? EntityId { get; set; }

        public string ShortDescription { get; set; }

        public string? Description { get; set; } = string.Empty;
    }
}

namespace Application.DTOs.Grade
{
    public record CreateGradeDto(
        decimal Value,
        string? Comment,
        Guid StudentId,
        Guid CourseId,
        Guid TeacherId);
}

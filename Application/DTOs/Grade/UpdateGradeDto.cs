namespace Application.DTOs.Grade
{
    public record UpdateGradeDto(
        Guid Id,
        decimal Value,
        string? Comment,
        Guid StudentId,
        Guid CourseId,
        Guid TeacherId);
}

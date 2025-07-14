namespace Application.DTOs.Grade
{
    public record GradeDto(
       Guid Id,
       decimal Value,
       DateTime DateReceived,
       string? Comment,
       bool IsActive);
}

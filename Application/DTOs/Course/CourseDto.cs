namespace Application.DTOs.Course
{
    public record CourseDto(
        Guid Id,
        string Name,
        bool IsActive);
}

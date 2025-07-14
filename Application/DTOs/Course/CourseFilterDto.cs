namespace Application.DTOs.Course
{
    public record CourseFilterDto(
        string? NameFilter = null,
        int Page = 1,
        int PageSize = 20);
}

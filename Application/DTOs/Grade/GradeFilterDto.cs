namespace Application.DTOs.Grade
{
    public record GradeFilterDto(
        string FirstName,
        string LastName,
        int Age,
        Guid DepartmentId);
}

namespace Application.DTOs.Student
{
    public record UpdateStudentDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Guid DepartmentId { get; set; }

    }
}

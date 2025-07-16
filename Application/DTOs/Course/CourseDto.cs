namespace Application.DTOs.Course
{
    public record CourseDto
    {
        public Guid Id { get;set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        
    }
}

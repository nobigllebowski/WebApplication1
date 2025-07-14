using Application.DTOs.Student;

namespace Application.DTOs.Department
{
    public class DepartmentDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public IList<StudentLiteDto> Students { get; set; } = new List<StudentLiteDto>();
    }
}

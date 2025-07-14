using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Teacher
{
    public record CreateTeacherDto
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        // Свойство для выбора факультета
        [Required]
        [Display(Name = "Факультет")]
        public Guid DepartmentId { get; set; }
    }
}

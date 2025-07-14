using Core.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Student
{
    public class CreateStudentDto
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Range(16, 100)]
        public int Age { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Дата рождения обязательна")]
        [MinimumAge(16, ErrorMessage = "Возраст должен быть не менее 16 лет")]
        [MaximumAge(100, ErrorMessage = "Возраст должен быть не более 100 лет")]
        public DateTime DateOfBirth { get; set; }

        // Свойство для выбора факультета
        [Required]
        [Display(Name = "Факультет")]
        public Guid DepartmentId { get; set; }
    }        
}

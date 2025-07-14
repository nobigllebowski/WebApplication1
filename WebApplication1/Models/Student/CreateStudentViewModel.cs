using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using Core.Attributes;

namespace WebApplication1.Models.Student
{
    public class CreateStudentViewModel
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Дата рождения обязательна")]
        [MinimumAge(16, ErrorMessage = "Возраст должен быть не менее 16 лет")]
        [MaximumAge(100, ErrorMessage = "Возраст должен быть не более 100 лет")]
        public DateTime DateOfBirth { get; set; }

        // Свойство для выбора факультета
        [Required]
        [Display(Name = "Факультет")]
        public Guid SelectedDepartmentId { get; set; }

        // Список доступных факультетов для dropdown
        public List<SelectListItem> Departments { get; set; } = new List<SelectListItem>();
    }
}

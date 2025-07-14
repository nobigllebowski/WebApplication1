using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models.Student
{
    public class EditStudentViewModel
    {
        public Guid Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
    }
}

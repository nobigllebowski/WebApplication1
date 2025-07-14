using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models.Teacher
{
    public class EditTeacherViewModel
    {
        public Guid Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
    }
}

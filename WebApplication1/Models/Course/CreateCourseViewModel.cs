using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models.Course
{
    public class CreateCourseViewModel
    {
        [Required]
        public string Name { get; set; }
    }
}

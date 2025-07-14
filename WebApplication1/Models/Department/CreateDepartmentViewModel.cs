using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models.Department
{
    public class CreateDepartmentViewModel
    {
        [Required]
        public string Name { get; set; }
    }
}

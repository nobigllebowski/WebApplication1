using Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    /// <summary>
    /// Логи
    /// </summary>
    public class LogEntry : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string EntityName { get; set; }
        public string? EntityId { get; set; } 

        [Required]
        [MaxLength(100)]
        public string ShortDescription { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }
    }
}

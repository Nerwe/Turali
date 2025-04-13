using System.ComponentModel.DataAnnotations;

namespace Turali.Models
{
    public class TransportType
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(50)]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Навигационные свойства
        public virtual ICollection<Order> Orders { get; set; } = [];
    }
}

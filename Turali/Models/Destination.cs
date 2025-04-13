using System.ComponentModel.DataAnnotations;

namespace Turali.Models
{
    public class Destination
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(50)]
        public string Country { get; set; } = string.Empty;

        [StringLength(50)]
        public string? City { get; set; }

        public string? Description { get; set; }

        // Навигационные свойства
        public virtual ICollection<Hotel> Hotels { get; set; } = [];
    }
}

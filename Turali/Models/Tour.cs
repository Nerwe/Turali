using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Turali.Models
{
    public class Tour
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required, StringLength(100)]
        public string Destination { get; set; } = string.Empty;

        public string? Description { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public int? Duration { get; set; }

        [Required]
        public decimal Price { get; set; }

        public int? MaxCapacity { get; set; }

        [StringLength(20)]
        public string Status { get; set; } = "active";

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [NotMapped]
        public string FullDisplay => $"{Name} {Destination}";

        // Навигационные свойства
        public virtual ICollection<Order> Orders { get; set; } = [];
        public virtual ICollection<Review> Reviews { get; set; } = [];
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Turali.Models
{
    public class Hotel
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [ForeignKey("Destination")]
        public int DestinationId { get; set; }

        public string? Address { get; set; }

        [Range(1, 5)]
        public int StarRating { get; set; }

        public string? Description { get; set; }

        [StringLength(20)]
        public string? ContactPhone { get; set; }

        [StringLength(100), EmailAddress]
        public string? ContactEmail { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Навигационные свойства
        public virtual Destination Destination { get; set; } = null!;
        public virtual ICollection<Room> Rooms { get; set; } = [];
        public virtual ICollection<HotelMealType> HotelMealTypes { get; set; } = [];
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Turali.Models
{
    public class Room
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Hotel")]
        public int HotelId { get; set; }

        [ForeignKey("RoomType")]
        public int RoomTypeId { get; set; }

        [Required, StringLength(20)]
        public string RoomNumber { get; set; } = string.Empty;

        [Required]
        public int Capacity { get; set; }

        public bool IsAvailable { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Навигационные свойства
        public virtual Hotel Hotel { get; set; } = null!;
        public virtual RoomType RoomType { get; set; } = null!;
        public virtual ICollection<Booking> Bookings { get; set; } = [];
    }
}

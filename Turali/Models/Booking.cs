using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Turali.Models
{
    public class Booking
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Order")]
        public int OrderId { get; set; }

        [ForeignKey("Client")]
        public int ClientId { get; set; }

        [ForeignKey("Room")]
        public int RoomId { get; set; }

        [ForeignKey("MealType")]
        public int? MealTypeId { get; set; }

        [Required]
        public DateTime CheckInDate { get; set; }

        [Required]
        public DateTime CheckOutDate { get; set; }

        public decimal? TotalPrice { get; set; }

        [StringLength(20)]
        public string Status { get; set; } = "pending";

        public string? Notes { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [NotMapped]
        public string HotelName { get; set; } = string.Empty;

        // Навигационные свойства
        public virtual Order Order { get; set; } = null!;
        public virtual Client Client { get; set; } = null!;
        public virtual Room Room { get; set; } = null!;
        public virtual MealType? MealType { get; set; }
    }
}

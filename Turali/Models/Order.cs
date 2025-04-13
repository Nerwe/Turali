using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Turali.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Client")]
        public int ClientId { get; set; }

        [ForeignKey("Tour")]
        public int TourId { get; set; }

        [ForeignKey("Manager")]
        public int ManagerId { get; set; }

        [ForeignKey("TransportType")]
        public int? TransportTypeId { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        [StringLength(20)]
        public string Status { get; set; } = "pending";

        public decimal? TotalPrice { get; set; }

        public decimal? Discount { get; set; } = 0;

        public string? Notes { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Навигационные свойства
        public virtual Client Client { get; set; } = null!;
        public virtual Tour Tour { get; set; } = null!;
        public virtual Manager Manager { get; set; } = null!;
        public virtual TransportType? TransportType { get; set; }
        public virtual ICollection<Document> Documents { get; set; } = [];
        public virtual ICollection<Payment> Payments { get; set; } = [];
        public virtual ICollection<Booking> Bookings { get; set; } = [];
    }
}

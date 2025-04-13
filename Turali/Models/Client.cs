using System.ComponentModel.DataAnnotations;

namespace Turali.Models
{
    public class Client
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Required, StringLength(50)]
        public string LastName { get; set; } = string.Empty;

        [StringLength(100), EmailAddress]
        public string? Email { get; set; }

        [StringLength(20)]
        public string? Phone { get; set; }

        public DateTime? BirthDate { get; set; }

        [StringLength(20)]
        public string? PassportNumber { get; set; }

        public DateTime? PassportExpiry { get; set; }

        public string? Address { get; set; }

        public string? Notes { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Навигационные свойства
        public virtual ICollection<Order> Orders { get; set; } = [];
        public virtual ICollection<Communication> Communications { get; set; } = [];
        public virtual ICollection<Document> Documents { get; set; } = [];
        public virtual ICollection<Payment> Payments { get; set; } = [];
        public virtual ICollection<Review> Reviews { get; set; } = [];
        public virtual ICollection<Booking> Bookings { get; set; } = [];
    }
}

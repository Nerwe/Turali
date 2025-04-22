using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Turali.Models
{
    public class Manager
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

        [StringLength(50)]
        public string? Role { get; set; }

        public DateTime? HireDate { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [NotMapped]
        public double? AverageRating { get; set; }
        [NotMapped]
        public string FullDisplay => $"{FirstName} {LastName} {Role}";

        // Навигационные свойства
        public virtual ICollection<Order> Orders { get; set; } = [];
        public virtual ICollection<Communication> Communications { get; set; } = [];
    }
}

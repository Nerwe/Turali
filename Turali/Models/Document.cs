using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Turali.Models
{
    public class Document
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Client")]
        public int ClientId { get; set; }

        [ForeignKey("Order")]
        public int? OrderId { get; set; }

        [Required, StringLength(20)]
        public string Type { get; set; } = string.Empty;

        [StringLength(255)]
        public string? FilePath { get; set; }

        public string? Details { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Навигационные свойства
        public virtual Client Client { get; set; } = null!;
        public virtual Order? Order { get; set; }
    }
}

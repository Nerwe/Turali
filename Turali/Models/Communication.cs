using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Turali.Models
{
    public class Communication
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Client")]
        public int ClientId { get; set; }

        [ForeignKey("Manager")]
        public int ManagerId { get; set; }

        [Required, StringLength(20)]
        public string Type { get; set; } = string.Empty;

        public DateTime Date { get; set; } = DateTime.UtcNow;

        public string? Description { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Навигационные свойства
        public virtual Client Client { get; set; } = null!;
        public virtual Manager Manager { get; set; } = null!;
    }
}

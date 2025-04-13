﻿using System.ComponentModel.DataAnnotations;

namespace Turali.Models
{
    public class RoomType
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(50)]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        [Required]
        public decimal BasePrice { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Навигационные свойства
        public virtual ICollection<Room> Rooms { get; set; } = [];
    }
}

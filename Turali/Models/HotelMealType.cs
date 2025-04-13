using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Turali.Models
{
    public class HotelMealType
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Hotel")]
        public int HotelId { get; set; }

        [ForeignKey("MealType")]
        public int MealTypeId { get; set; }

        public decimal? AdditionalCost { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Навигационные свойства
        public virtual Hotel Hotel { get; set; } = null!;
        public virtual MealType MealType { get; set; } = null!;
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Turali.Models;

namespace Turali.Configurations
{
    public class HotelMealTypeConfiguration : IEntityTypeConfiguration<HotelMealType>
    {
        public void Configure(EntityTypeBuilder<HotelMealType> builder)
        {
            // Указание имени таблицы
            builder.ToTable("HotelMealTypes");

            // Первичный ключ
            builder.HasKey(hmt => hmt.Id);

            // Связь с Hotel
            builder.HasOne(hmt => hmt.Hotel)
                .WithMany(h => h.HotelMealTypes)
                .HasForeignKey(hmt => hmt.HotelId)
                .OnDelete(DeleteBehavior.Cascade);

            // Связь с MealType
            builder.HasOne(hmt => hmt.MealType)
                .WithMany(mt => mt.HotelMealTypes)
                .HasForeignKey(hmt => hmt.MealTypeId)
                .OnDelete(DeleteBehavior.Cascade);

            // Дополнительные свойства
            builder.Property(hmt => hmt.AdditionalCost)
                .HasColumnType("decimal(18,2)")
                .IsRequired(false);

            builder.Property(hmt => hmt.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()")
                .IsRequired();
        }
    }
}

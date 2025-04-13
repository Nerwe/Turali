using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Turali.Models;

namespace Turali.Configurations
{
    public class MealTypeConfiguration : IEntityTypeConfiguration<MealType>
    {
        public void Configure(EntityTypeBuilder<MealType> builder)
        {
            // Указание имени таблицы
            builder.ToTable("MealTypes");

            // Первичный ключ
            builder.HasKey(mt => mt.Id);

            // Свойства
            builder.Property(mt => mt.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(mt => mt.Description)
                .HasMaxLength(255);

            builder.Property(mt => mt.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()")
                .IsRequired();

            // Связь с HotelMealType
            builder.HasMany(mt => mt.HotelMealTypes)
                .WithOne(hmt => hmt.MealType)
                .HasForeignKey(hmt => hmt.MealTypeId)
                .OnDelete(DeleteBehavior.Cascade);

            // Связь с Booking
            builder.HasMany(mt => mt.Bookings)
                .WithOne(b => b.MealType)
                .HasForeignKey(b => b.MealTypeId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}

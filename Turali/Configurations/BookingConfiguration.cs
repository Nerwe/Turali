using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Turali.Models;

namespace Turali.Configurations
{
    public class BookingConfiguration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            // Указание имени таблицы
            builder.ToTable("Bookings");

            // Первичный ключ
            builder.HasKey(b => b.Id);

            // Свойства
            builder.Property(b => b.CheckInDate)
                .IsRequired();

            builder.Property(b => b.CheckOutDate)
                .IsRequired();

            builder.Property(b => b.TotalPrice)
                .HasColumnType("decimal(18,2)")
                .IsRequired(false);

            builder.Property(b => b.Status)
                .HasMaxLength(20)
                .HasDefaultValue("pending")
                .IsRequired();

            builder.Property(b => b.Notes)
                .HasMaxLength(500)
                .IsRequired(false);

            builder.Property(b => b.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()")
                .IsRequired();

            // Связь с Order
            builder.HasOne(b => b.Order)
                .WithMany(o => o.Bookings)
                .HasForeignKey(b => b.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            // Связь с Client
            builder.HasOne(b => b.Client)
                .WithMany(c => c.Bookings)
                .HasForeignKey(b => b.ClientId)
                .OnDelete(DeleteBehavior.Restrict);

            // Связь с Room
            builder.HasOne(b => b.Room)
                .WithMany(r => r.Bookings)
                .HasForeignKey(b => b.RoomId)
                .OnDelete(DeleteBehavior.Restrict);

            // Связь с MealType
            builder.HasOne(b => b.MealType)
                .WithMany(mt => mt.Bookings)
                .HasForeignKey(b => b.MealTypeId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}

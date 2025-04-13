using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Turali.Models;

namespace Turali.Configurations
{
    public class RoomConfiguration : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            // Указание имени таблицы
            builder.ToTable("Rooms");

            // Первичный ключ
            builder.HasKey(r => r.Id);

            // Свойства
            builder.Property(r => r.RoomNumber)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(r => r.Capacity)
                .IsRequired();

            builder.Property(r => r.IsAvailable)
                .HasDefaultValue(true)
                .IsRequired();

            builder.Property(r => r.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()")
                .IsRequired();

            // Связь с Hotel
            builder.HasOne(r => r.Hotel)
                .WithMany(h => h.Rooms)
                .HasForeignKey(r => r.HotelId)
                .OnDelete(DeleteBehavior.Cascade);

            // Связь с RoomType
            builder.HasOne(r => r.RoomType)
                .WithMany(rt => rt.Rooms)
                .HasForeignKey(r => r.RoomTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            // Связь с Booking
            builder.HasMany(r => r.Bookings)
                .WithOne(b => b.Room)
                .HasForeignKey(b => b.RoomId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

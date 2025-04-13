using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Turali.Models;

namespace Turali.Configurations
{
    public class HotelConfiguration : IEntityTypeConfiguration<Hotel>
    {
        public void Configure(EntityTypeBuilder<Hotel> builder)
        {
            // Указание имени таблицы
            builder.ToTable("Hotels");

            // Первичный ключ
            builder.HasKey(h => h.Id);

            // Свойства
            builder.Property(h => h.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(h => h.Address)
                .HasMaxLength(255)
                .IsRequired(false);

            builder.Property(h => h.StarRating)
                .IsRequired();

            builder.Property(h => h.Description)
                .HasMaxLength(1000)
                .IsRequired(false);

            builder.Property(h => h.ContactPhone)
                .HasMaxLength(20)
                .IsRequired(false);

            builder.Property(h => h.ContactEmail)
                .HasMaxLength(100)
                .IsUnicode(false)
                .IsRequired(false);

            builder.Property(h => h.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()")
                .IsRequired();

            // Связь с Destination
            builder.HasOne(h => h.Destination)
                .WithMany(d => d.Hotels)
                .HasForeignKey(h => h.DestinationId)
                .OnDelete(DeleteBehavior.Cascade);

            // Связь с Room
            builder.HasMany(h => h.Rooms)
                .WithOne(r => r.Hotel)
                .HasForeignKey(r => r.HotelId)
                .OnDelete(DeleteBehavior.Cascade);

            // Связь с HotelMealType
            builder.HasMany(h => h.HotelMealTypes)
                .WithOne(hmt => hmt.Hotel)
                .HasForeignKey(hmt => hmt.HotelId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}


using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Turali.Models;

namespace Turali.Configurations
{
    public class DestinationConfiguration : IEntityTypeConfiguration<Destination>
    {
        public void Configure(EntityTypeBuilder<Destination> builder)
        {
            // Указание имени таблицы
            builder.ToTable("Destinations");

            // Первичный ключ
            builder.HasKey(d => d.Id);

            // Свойства
            builder.Property(d => d.Country)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(d => d.City)
                .HasMaxLength(50)
                .IsRequired(false);

            builder.Property(d => d.Description)
                .HasColumnType("text")
                .IsRequired(false);

            // Связь с Hotel
            builder.HasMany(d => d.Hotels)
                .WithOne(h => h.Destination)
                .HasForeignKey(h => h.DestinationId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}


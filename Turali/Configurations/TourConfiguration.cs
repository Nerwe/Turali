using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Turali.Models;

namespace Turali.Configurations
{
    public class TourConfiguration : IEntityTypeConfiguration<Tour>
    {
        public void Configure(EntityTypeBuilder<Tour> builder)
        {
            // Указание имени таблицы
            builder.ToTable("Tours");

            // Первичный ключ
            builder.HasKey(t => t.Id);

            // Свойства
            builder.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(t => t.Destination)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(t => t.Description)
                .HasMaxLength(1000)
                .IsRequired(false);

            builder.Property(t => t.StartDate)
                .IsRequired(false);

            builder.Property(t => t.EndDate)
                .IsRequired(false);

            builder.Property(t => t.Duration)
                .IsRequired(false);

            builder.Property(t => t.Price)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(t => t.MaxCapacity)
                .IsRequired(false);

            builder.Property(t => t.Status)
                .HasMaxLength(20)
                .HasDefaultValue("active")
                .IsRequired();

            builder.Property(t => t.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()")
                .IsRequired();

            // Связь с Order
            builder.HasMany(t => t.Orders)
                .WithOne(o => o.Tour)
                .HasForeignKey(o => o.TourId)
                .OnDelete(DeleteBehavior.Restrict);

            // Связь с Review
            builder.HasMany(t => t.Reviews)
                .WithOne(r => r.Tour)
                .HasForeignKey(r => r.TourId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

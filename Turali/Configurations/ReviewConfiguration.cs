using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Turali.Models;

namespace Turali.Configurations
{
    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            // Указание имени таблицы
            builder.ToTable("Reviews");

            // Первичный ключ
            builder.HasKey(r => r.Id);

            // Свойства
            builder.Property(r => r.Rating)
                .IsRequired();

            builder.Property(r => r.Comment)
                .HasMaxLength(1000)
                .IsRequired(false);

            builder.Property(r => r.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()")
                .IsRequired();

            // Связь с Client
            builder.HasOne(r => r.Client)
                .WithMany(c => c.Reviews)
                .HasForeignKey(r => r.ClientId)
                .OnDelete(DeleteBehavior.Restrict);

            // Связь с Tour
            builder.HasOne(r => r.Tour)
                .WithMany(t => t.Reviews)
                .HasForeignKey(r => r.TourId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Turali.Models;

namespace Turali.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            // Указание имени таблицы
            builder.ToTable("Orders");

            // Первичный ключ
            builder.HasKey(o => o.Id);

            // Свойства
            builder.Property(o => o.OrderDate)
                .HasDefaultValueSql("GETUTCDATE()")
                .IsRequired();

            builder.Property(o => o.Status)
                .HasMaxLength(20)
                .HasDefaultValue("pending")
                .IsRequired();

            builder.Property(o => o.TotalPrice)
                .HasColumnType("decimal(18,2)")
                .IsRequired(false);

            builder.Property(o => o.Discount)
                .HasColumnType("decimal(18,2)")
                .HasDefaultValue(0m)
                .IsRequired(false);

            builder.Property(o => o.Notes)
                .HasMaxLength(500)
                .IsRequired(false);

            builder.Property(o => o.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()")
                .IsRequired();

            // Связь с Client
            builder.HasOne(o => o.Client)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.ClientId)
                .OnDelete(DeleteBehavior.Restrict);

            // Связь с Tour
            builder.HasOne(o => o.Tour)
                .WithMany()
                .HasForeignKey(o => o.TourId)
                .OnDelete(DeleteBehavior.Restrict);

            // Связь с Manager
            builder.HasOne(o => o.Manager)
                .WithMany(m => m.Orders)
                .HasForeignKey(o => o.ManagerId)
                .OnDelete(DeleteBehavior.Restrict);

            // Связь с TransportType
            builder.HasOne(o => o.TransportType)
                .WithMany()
                .HasForeignKey(o => o.TransportTypeId)
                .OnDelete(DeleteBehavior.SetNull);

            // Связь с Document
            builder.HasMany(o => o.Documents)
                .WithOne(d => d.Order)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            // Связь с Payment
            builder.HasMany(o => o.Payments)
                .WithOne(p => p.Order)
                .HasForeignKey(p => p.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            // Связь с Booking
            builder.HasMany(o => o.Bookings)
                .WithOne(b => b.Order)
                .HasForeignKey(b => b.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

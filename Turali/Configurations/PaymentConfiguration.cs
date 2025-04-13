using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Turali.Models;

namespace Turali.Configurations
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            // Указание имени таблицы
            builder.ToTable("Payments");

            // Первичный ключ
            builder.HasKey(p => p.Id);

            // Свойства
            builder.Property(p => p.Amount)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(p => p.PaymentDate)
                .HasDefaultValueSql("GETUTCDATE()")
                .IsRequired();

            builder.Property(p => p.Method)
                .HasMaxLength(20)
                .HasDefaultValue("credit_card")
                .IsRequired();

            builder.Property(p => p.Status)
                .HasMaxLength(20)
                .HasDefaultValue("pending")
                .IsRequired(false);

            builder.Property(p => p.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()")
                .IsRequired();

            // Связь с Order
            builder.HasOne(p => p.Order)
                .WithMany(o => o.Payments)
                .HasForeignKey(p => p.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            // Связь с Client
            builder.HasOne(p => p.Client)
                .WithMany(c => c.Payments)
                .HasForeignKey(p => p.ClientId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

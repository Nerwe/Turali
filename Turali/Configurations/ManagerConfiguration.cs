using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Turali.Models;

namespace Turali.Configurations
{
    public class ManagerConfiguration : IEntityTypeConfiguration<Manager>
    {
        public void Configure(EntityTypeBuilder<Manager> builder)
        {
            // Указание имени таблицы
            builder.ToTable("Managers");

            // Первичный ключ
            builder.HasKey(m => m.Id);

            // Свойства
            builder.Property(m => m.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(m => m.LastName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(m => m.Email)
                .HasMaxLength(100);

            builder.Property(m => m.Phone)
                .HasMaxLength(20);

            builder.Property(m => m.Role)
                .HasMaxLength(50);

            builder.Property(m => m.HireDate)
                .IsRequired(false);

            builder.Property(m => m.IsActive)
                .HasDefaultValue(true);

            builder.Property(m => m.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()")
                .IsRequired();

            // Связь с Order
            builder.HasMany(m => m.Orders)
                .WithOne(o => o.Manager)
                .HasForeignKey(o => o.ManagerId)
                .OnDelete(DeleteBehavior.Restrict);

            // Связь с Communication
            builder.HasMany(m => m.Communications)
                .WithOne(c => c.Manager)
                .HasForeignKey(c => c.ManagerId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

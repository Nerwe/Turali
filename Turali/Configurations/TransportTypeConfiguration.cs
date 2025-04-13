using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Turali.Models;

namespace Turali.Configurations
{
    public class TransportTypeConfiguration : IEntityTypeConfiguration<TransportType>
    {
        public void Configure(EntityTypeBuilder<TransportType> builder)
        {
            // Указание имени таблицы
            builder.ToTable("TransportTypes");

            // Первичный ключ
            builder.HasKey(tt => tt.Id);

            // Свойства
            builder.Property(tt => tt.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(tt => tt.Description)
                .HasMaxLength(255)
                .IsRequired(false);

            builder.Property(tt => tt.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()")
                .IsRequired();

            // Связь с Order
            builder.HasMany(tt => tt.Orders)
                .WithOne(o => o.TransportType)
                .HasForeignKey(o => o.TransportTypeId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}

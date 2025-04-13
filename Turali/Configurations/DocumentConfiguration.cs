using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Turali.Models;

namespace Turali.Configurations
{
    public class DocumentConfiguration : IEntityTypeConfiguration<Document>
    {
        public void Configure(EntityTypeBuilder<Document> builder)
        {
            // Указание имени таблицы
            builder.ToTable("Documents");

            // Первичный ключ
            builder.HasKey(d => d.Id);

            // Свойства
            builder.Property(d => d.Type)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(d => d.FilePath)
                .HasMaxLength(255)
                .IsRequired(false);

            builder.Property(d => d.Details)
                .HasMaxLength(500)
                .IsRequired(false);

            builder.Property(d => d.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()")
                .IsRequired();

            // Связь с Client
            builder.HasOne(d => d.Client)
                .WithMany(c => c.Documents)
                .HasForeignKey(d => d.ClientId)
                .OnDelete(DeleteBehavior.Cascade);

            // Связь с Order
            builder.HasOne(d => d.Order)
                .WithMany(o => o.Documents)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}


using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Turali.Models;

namespace Turali.Configurations
{
    public class CommunicationConfiguration : IEntityTypeConfiguration<Communication>
    {
        public void Configure(EntityTypeBuilder<Communication> builder)
        {
            // Указание имени таблицы
            builder.ToTable("Communications");

            // Первичный ключ
            builder.HasKey(c => c.Id);

            // Свойства
            builder.Property(c => c.Type)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(c => c.Date)
                .HasDefaultValueSql("GETUTCDATE()")
                .IsRequired();

            builder.Property(c => c.Description)
                .HasMaxLength(500)
                .IsRequired(false);

            builder.Property(c => c.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()")
                .IsRequired();

            // Связь с Client
            builder.HasOne(c => c.Client)
                .WithMany(cl => cl.Communications)
                .HasForeignKey(c => c.ClientId)
                .OnDelete(DeleteBehavior.Cascade);

            // Связь с Manager
            builder.HasOne(c => c.Manager)
                .WithMany(m => m.Communications)
                .HasForeignKey(c => c.ManagerId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}


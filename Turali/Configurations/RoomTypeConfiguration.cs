using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Turali.Models;

namespace Turali.Configurations
{
    public class RoomTypeConfiguration : IEntityTypeConfiguration<RoomType>
    {
        public void Configure(EntityTypeBuilder<RoomType> builder)
        {
            // Указание имени таблицы
            builder.ToTable("RoomTypes");

            // Первичный ключ
            builder.HasKey(rt => rt.Id);

            // Свойства
            builder.Property(rt => rt.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(rt => rt.Description)
                .HasMaxLength(255)
                .IsRequired(false);

            builder.Property(rt => rt.BasePrice)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(rt => rt.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()")
                .IsRequired();

            // Связь с Room
            builder.HasMany(rt => rt.Rooms)
                .WithOne(r => r.RoomType)
                .HasForeignKey(r => r.RoomTypeId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

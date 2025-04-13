using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Turali.Models;

namespace Turali.Configurations
{
    public class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            // Указание имени таблицы
            builder.ToTable("Clients");

            // Первичный ключ
            builder.HasKey(c => c.Id);

            // Свойства
            builder.Property(c => c.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(c => c.LastName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(c => c.Email)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(c => c.Phone)
                .HasMaxLength(20)
                .IsUnicode(false);

            builder.Property(c => c.PassportNumber)
                .HasMaxLength(20)
                .IsUnicode(false);

            builder.Property(c => c.Address)
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(c => c.Notes)
                .HasMaxLength(500)
                .IsUnicode(false);

            builder.Property(c => c.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()")
                .IsRequired();

            builder.Property(c => c.UpdatedAt)
                .HasDefaultValueSql("GETUTCDATE()")
                .ValueGeneratedOnAddOrUpdate()
                .IsRequired();

            // Связь с Order
            builder.HasMany(c => c.Orders)
                .WithOne(o => o.Client)
                .HasForeignKey(o => o.ClientId)
                .OnDelete(DeleteBehavior.Restrict);

            // Связь с Communication
            builder.HasMany(c => c.Communications)
                .WithOne(comm => comm.Client)
                .HasForeignKey(comm => comm.ClientId)
                .OnDelete(DeleteBehavior.Cascade);

            // Связь с Document
            builder.HasMany(c => c.Documents)
                .WithOne(d => d.Client)
                .HasForeignKey(d => d.ClientId)
                .OnDelete(DeleteBehavior.Cascade);

            // Связь с Payment
            builder.HasMany(c => c.Payments)
                .WithOne(p => p.Client)
                .HasForeignKey(p => p.ClientId)
                .OnDelete(DeleteBehavior.Cascade);

            // Связь с Review
            builder.HasMany(c => c.Reviews)
                .WithOne(r => r.Client)
                .HasForeignKey(r => r.ClientId)
                .OnDelete(DeleteBehavior.Cascade);

            // Связь с Booking
            builder.HasMany(c => c.Bookings)
                .WithOne(b => b.Client)
                .HasForeignKey(b => b.ClientId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}


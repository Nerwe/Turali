using Microsoft.EntityFrameworkCore;
using Turali.Configurations;
using Turali.Models;

namespace Turali.Data
{
    public class TuraliDBContext(DbContextOptions<TuraliDBContext> options) : DbContext(options)
    {
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Communication> Communications { get; set; }
        public DbSet<Destination> Destinations { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<HotelMealType> HotelMealTypes { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<MealType> MealTypes { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomType> RoomTypes { get; set; }
        public DbSet<Tour> Tours { get; set; }
        public DbSet<TransportType> TransportTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new BookingConfiguration());
            modelBuilder.ApplyConfiguration(new ClientConfiguration());
            modelBuilder.ApplyConfiguration(new CommunicationConfiguration());
            modelBuilder.ApplyConfiguration(new DestinationConfiguration());
            modelBuilder.ApplyConfiguration(new DocumentConfiguration());
            modelBuilder.ApplyConfiguration(new HotelConfiguration());
            modelBuilder.ApplyConfiguration(new HotelMealTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ManagerConfiguration());
            modelBuilder.ApplyConfiguration(new MealTypeConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new PaymentConfiguration());
            modelBuilder.ApplyConfiguration(new ReviewConfiguration());
            modelBuilder.ApplyConfiguration(new RoomConfiguration());
            modelBuilder.ApplyConfiguration(new RoomTypeConfiguration());
            modelBuilder.ApplyConfiguration(new TourConfiguration());
            modelBuilder.ApplyConfiguration(new TransportTypeConfiguration());
        }
    }
}
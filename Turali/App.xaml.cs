using System.Windows;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Turali.Data;
using Turali.Repositories;
using System.IO;

namespace Turali
{
    public partial class App : Application
    {
        private readonly IHost _host;

        public App()
        {
            _host = Host.CreateDefaultBuilder()
                .ConfigureAppConfiguration((context, config) =>
                {
                    config.SetBasePath(Directory.GetCurrentDirectory());
                    config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                })
                .ConfigureServices((context, services) =>
                {
                    string? connectionString = context.Configuration.GetConnectionString("TuraliDbContext");

                    if (string.IsNullOrEmpty(connectionString))
                    {
                        throw new InvalidOperationException("TuraliDbContext is not configured properly.");
                    }

                    services.AddDbContext<TuraliDBContext>(options =>
                    {
                        options.UseSqlServer(connectionString);
                    });

                    services.AddScoped<IBookingRepository, BookingRepository>();
                    services.AddScoped<IClientRepository, ClientRepository>();
                    services.AddScoped<ICommunicationRepository, CommunicationRepository>();
                    services.AddScoped<IDestinationRepository, DestinationRepository>();
                    services.AddScoped<IDocumentRepository, DocumentRepository>();
                    services.AddScoped<IHotelMealTypeRepository, HotelMealTypeRepository>();
                    services.AddScoped<IHotelRepository, HotelRepository>();
                    services.AddScoped<IManagerRepository, ManagerRepository>();
                    services.AddScoped<IMealTypeRepository, MealTypeRepository>();
                    services.AddScoped<IOrderRepository, OrderRepository>();
                    services.AddScoped<IPaymentRepository, PaymentRepository>();
                    services.AddScoped<IReviewRepository, ReviewRepository>();
                    services.AddScoped<IRoomTypeRepository, RoomTypeRepository>();
                    services.AddScoped<IRoomRepository, RoomRepository>();
                    services.AddScoped<ITourRepository, TourRepository>();
                    services.AddScoped<ITransportTypeRepository, TransportTypeRepository>();
                    services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
                })
                .Build();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await _host.StartAsync();
            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            await _host.StopAsync();
            _host.Dispose();
            base.OnExit(e);
        }

        public static T? GetService<T>() where T : class => (Current as App)?._host.Services.GetService<T>();
    }
}

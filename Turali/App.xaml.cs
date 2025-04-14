using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IO;
using System.Windows;
using Turali.Data;
using Turali.Helpers;
using Turali.Repositories;
using Turali.ViewModels;
using Turali.ViewModels.Client;
using Turali.ViewModels.Manager;
using Turali.ViewModels.Order;
using Turali.ViewModels.Tour;
using Turali.Views;

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
                    config.AddUserSecrets<App>(optional: true, reloadOnChange: true);
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

                    services.AddSingleton<CurrentClient>();
                    services.AddSingleton<CurrentManager>();
                    services.AddSingleton<CurrentTour>();
                    services.AddSingleton<CurrentOrder>();

                    services.AddTransient<MainViewModel>();
                    services.AddTransient<DashboardViewModel>();
                    services.AddTransient<ClientsViewModel>();
                    services.AddTransient<ManagersViewModel>();
                    services.AddTransient<ToursViewModel>();
                    services.AddTransient<OrdersViewModel>();
                    services.AddTransient<NewClientViewModel>();
                    services.AddTransient<NewManagerViewModel>();
                    services.AddTransient<NewTourViewModel>();
                    services.AddTransient<NewOrderViewModel>();
                    services.AddTransient<ClientDetailsViewModel>();
                    services.AddTransient<ManagerDetailsViewModel>();
                    services.AddTransient<TourDetailsViewModel>();
                    services.AddTransient<OrderDetailsViewModel>();
                })
                .Build();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await _host.StartAsync();

            var mainViewModel = _host.Services.GetRequiredService<MainViewModel>();
            var mainWindow = new MainView
            {
                DataContext = mainViewModel
            };
            mainWindow.Show();

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

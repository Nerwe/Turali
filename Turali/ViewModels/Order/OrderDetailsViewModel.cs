using System.Collections.ObjectModel;
using System.Windows.Input;
using Turali.Base;
using Turali.Helpers;
using Turali.Repositories;

namespace Turali.ViewModels.Order
{
    public class OrderDetailsViewModel : BaseViewModel
    {
        private readonly DashboardViewModel _dashboardViewModel;

        private readonly IOrderRepository _orderRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IManagerRepository _managerRepository;
        private readonly ITourRepository _tourRepository;
        private readonly IBookingRepository _bookingRepository;

        private readonly CurrentOrder _currentOrder;
        private readonly CurrentBooking _currentBooking;
        private readonly CurrentClient _currentClient;
        private readonly CurrentManager _currentManager;
        private readonly CurrentTour _currentTour;

        private Models.Order _order = new();
        private Models.Client _client = new();
        private Models.Manager _manager = new();
        private Models.Tour _tour = new();
        private Models.Booking _booking = new();

        private ObservableCollection<Models.Booking> _bookings = [];

        public Models.Order Order
        {
            get => _order;
            set
            {
                _order = value;
                OnPropertyChanged(nameof(Order));
            }
        }

        public Models.Client Client
        {
            get => _client;
            set
            {
                _client = value;
                OnPropertyChanged(nameof(Client));
            }
        }

        public Models.Manager Manager
        {
            get => _manager;
            set
            {
                _manager = value;
                OnPropertyChanged(nameof(Manager));
            }
        }

        public Models.Tour Tour
        {
            get => _tour;
            set
            {
                _tour = value;
                OnPropertyChanged(nameof(Tour));
            }
        }

        public Models.Booking Booking
        {
            get => _booking;
            set
            {
                _booking = value;
                OnPropertyChanged(nameof(Booking));
            }
        }

        public ObservableCollection<Models.Booking> Bookings
        {
            get => _bookings;
            set
            {
                _bookings = value;
                OnPropertyChanged(nameof(Bookings));
            }
        }

        public ICommand ShowOrderDetailsCommand { get; }

        public ICommand ShowClientDetailsViewCommand { get; }
        public ICommand ShowManagerDetailsViewCommand { get; }
        public ICommand ShowTourDetailsViewCommand { get; }
        public ICommand ShowBookingDetailsViewCommand { get; }
        public ICommand ShowNewBookingViewCommand { get; }

        public OrderDetailsViewModel(
            DashboardViewModel dashboardViewModel,
            IOrderRepository orderRepository,
            IClientRepository clientRepository,
            IManagerRepository managerRepository,
            ITourRepository tourRepository,
            IBookingRepository bookingRepository,
            CurrentOrder currentOrder,
            CurrentBooking currentBooking,
            CurrentClient currentClient,
            CurrentManager currentManager,
            CurrentTour currentTour)
        {
            _dashboardViewModel = dashboardViewModel;

            _orderRepository = orderRepository;
            _clientRepository = clientRepository;
            _managerRepository = managerRepository;
            _tourRepository = tourRepository;
            _bookingRepository = bookingRepository;

            _currentOrder = currentOrder;
            _currentBooking = currentBooking;
            _currentClient = currentClient;
            _currentManager = currentManager;
            _currentTour = currentTour;

            ShowOrderDetailsCommand = new SyncCommand(ExecuteShowOrderDetailsCommand);

            ShowClientDetailsViewCommand = new SyncCommand(ExecuteShowClientDetailsViewCommand);
            ShowManagerDetailsViewCommand = new SyncCommand(ExecuteShowManagerDetailsViewCommand);
            ShowTourDetailsViewCommand = new SyncCommand(ExecuteShowTourDetailsViewCommand);
            ShowBookingDetailsViewCommand = new SyncCommand(ExecuteShowBookingDetailsViewCommand);
            ShowNewBookingViewCommand = new SyncCommand(ExecuteShowNewBookingViewCommand);

            ShowOrderDetailsCommand.Execute(new object());

            LoadData();
        }

        private void ExecuteShowNewBookingViewCommand(object obj)
        {
            _dashboardViewModel.NewBookingViewCommand.Execute(null);
        }

        private void ExecuteShowBookingDetailsViewCommand(object obj)
        {
            if (obj is Models.Booking booking)
            {
                _currentBooking.Booking = booking;
                _dashboardViewModel.BookingDetailsViewCommand.Execute(null);
            }
        }

        private void ExecuteShowTourDetailsViewCommand(object obj)
        {
            if (obj is Models.Tour tour)
            {
                _currentTour.Tour = tour;
                _dashboardViewModel.TourDetailsViewCommand.Execute(null);
            }
        }

        private void ExecuteShowManagerDetailsViewCommand(object obj)
        {
            if (obj is Models.Manager manager)
            {
                _currentManager.Manager = manager;
                _dashboardViewModel.ManagerDetailsViewCommand.Execute(null);
            }
        }

        private void ExecuteShowClientDetailsViewCommand(object obj)
        {
            if (obj is Models.Client client)
            {
                _currentClient.Client = client;
                _dashboardViewModel.ClientDetailsViewCommand.Execute(null);
            }
        }

        private void ExecuteShowOrderDetailsCommand(object obj)
        {
            if (_currentOrder.Order == null) return;

            Order = _currentOrder.Order;
        }

        private async void LoadData()
        {
            if (Order.ClientId > 0)
            {
                var client = await _clientRepository.GetByIdAsync(Order.ClientId);
                if (client != null)
                {
                    Client = client;
                }
            }

            if (Order.TourId > 0)
            {
                var tour = await _tourRepository.GetByIdAsync(Order.TourId);
                if (tour != null)
                {
                    Tour = tour;
                }
            }

            if (Order.ManagerId > 0)
            {
                var manager = await _managerRepository.GetByIdAsync(Order.ManagerId);
                if (manager != null)
                {
                    Manager = manager;
                }
            }

            var bookings = await _bookingRepository.GetBookingsByOrderIdAsync(Order.Id);
            if (bookings != null)
            {
                Bookings = [.. bookings];
            }
        }
    }
}

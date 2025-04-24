using System.Windows.Input;
using Turali.Base;
using Turali.Helpers;
using Turali.Repositories;
using Turali.ViewModels.Booking;
using Turali.ViewModels.Client;
using Turali.ViewModels.Manager;
using Turali.ViewModels.Order;
using Turali.ViewModels.Tour;

namespace Turali.ViewModels
{
    public class DashboardViewModel : BaseViewModel
    {
        private BaseViewModel _currentViewModel = null!;

        private readonly IClientRepository _clientRepository = null!;
        private readonly IManagerRepository _managerRepository = null!;
        private readonly ITourRepository _tourRepository = null!;
        private readonly IOrderRepository _orderRepository = null!;
        private readonly IReviewRepository _reviewRepository = null!;
        private readonly ITransportTypeRepository _transportTypeRepository = null!;
        private readonly IBookingRepository _bookingRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly IMealTypeRepository _mealTypeRepository;
        private readonly IHotelRepository _hotelRepository;

        private readonly CurrentClient _currentClient = null!;
        private readonly CurrentManager _currentManager = null!;
        private readonly CurrentTour _currentTour = null!;
        private readonly CurrentOrder _currentOrder = null!;
        private readonly CurrentBooking _currentBooking = null!;

        private string _title = string.Empty;

        public BaseViewModel CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel = value;
                OnPropertyChanged(nameof(CurrentViewModel));
            }
        }

        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged(nameof(Title));
            }
        }

        //Clients
        public ICommand ClientDetailsViewCommand { get; }
        public ICommand ClientsViewCommand { get; }
        public ICommand NewClientViewCommand { get; }


        //Managers
        public ICommand ManagerDetailsViewCommand { get; }
        public ICommand ManagersViewCommand { get; }
        public ICommand NewManagerViewCommand { get; }

        //Tours
        public ICommand TourDetailsViewCommand { get; }
        public ICommand ToursViewCommand { get; }
        public ICommand NewTourViewCommand { get; }

        //Orders
        public ICommand OrderDetailsViewCommand { get; }
        public ICommand OrdersViewCommand { get; }
        public ICommand NewOrderViewCommand { get; }

        //Bookings
        public ICommand BookingDetailsViewCommand { get; }
        public ICommand BookingsViewCommand { get; }
        public ICommand NewBookingViewCommand { get; }

        public DashboardViewModel(
            IClientRepository clientRepository,
            IManagerRepository managerRepository,
            ITourRepository tourRepository,
            IOrderRepository orderRepository,
            IReviewRepository reviewRepository,
            CurrentClient currentClient,
            CurrentManager currentManager,
            CurrentTour currentTour,
            CurrentOrder currentOrder,
            CurrentBooking currentBooking,
            ITransportTypeRepository transportTypeRepository,
            IBookingRepository bookingRepository,
            IRoomRepository roomRepository,
            IMealTypeRepository mealTypeRepository,
            IHotelRepository hotelRepository)
        {
            _currentViewModel = new BaseViewModel();

            _clientRepository = clientRepository;
            _managerRepository = managerRepository;
            _tourRepository = tourRepository;
            _orderRepository = orderRepository;
            _reviewRepository = reviewRepository;
            _transportTypeRepository = transportTypeRepository;
            _bookingRepository = bookingRepository;
            _roomRepository = roomRepository;
            _mealTypeRepository = mealTypeRepository;
            _hotelRepository = hotelRepository;

            _currentClient = currentClient;
            _currentManager = currentManager;
            _currentTour = currentTour;
            _currentOrder = currentOrder;
            _currentBooking = currentBooking;

            ClientsViewCommand = new SyncCommand(ExecuteClientsViewCommand);
            ClientDetailsViewCommand = new SyncCommand(ExecuteClientDetailsViewCommand);
            NewClientViewCommand = new SyncCommand(ExecuteNewClientViewCommand);

            ManagersViewCommand = new SyncCommand(ExecuteManagersViewCommand);
            ManagerDetailsViewCommand = new SyncCommand(ExecuteManagerDetailsCommand);
            NewManagerViewCommand = new SyncCommand(ExecuteNewManagerViewCommand);

            ToursViewCommand = new SyncCommand(ExecuteToursViewCommand);
            TourDetailsViewCommand = new SyncCommand(ExecuteTourDetailsViewCommand);
            NewTourViewCommand = new SyncCommand(ExecuteNewTourViewCommand);

            OrdersViewCommand = new SyncCommand(ExecuteOrdersViewCommand);
            OrderDetailsViewCommand = new SyncCommand(ExecuteOrderDetailsViewCommand);
            NewOrderViewCommand = new SyncCommand(ExecuteNewOrderViewCommand);

            BookingDetailsViewCommand = new SyncCommand(ExecuteBookingDetailsViewCommand);
            BookingsViewCommand = new SyncCommand(ExecuteBookingsViewCommand);
            NewBookingViewCommand = new SyncCommand(ExecuteNewBookingViewCommand);

            ClientsViewCommand.Execute(null);
            _reviewRepository = reviewRepository;
        }

        private void ExecuteNewBookingViewCommand(object obj)
        {
            Title = "New Booking";
            CurrentViewModel = new NewBookingViewModel(this, _bookingRepository, _clientRepository, _roomRepository, _orderRepository, _hotelRepository, _mealTypeRepository, _currentOrder);
        }

        private void ExecuteBookingsViewCommand(object obj)
        {
            Title = "Bookings";
            CurrentViewModel = new BookingsViewModel(this, _bookingRepository, _roomRepository, _clientRepository, _orderRepository, _mealTypeRepository, _hotelRepository, _currentBooking);
        }

        private void ExecuteBookingDetailsViewCommand(object obj)
        {
            Title = "Booking Details";
            CurrentViewModel = new BookingDetailsViewModel(_clientRepository, _roomRepository, _orderRepository, _mealTypeRepository, _hotelRepository, _currentBooking);
        }

        private void ExecuteClientsViewCommand(object obj)
        {
            Title = "Clients";
            CurrentViewModel = new ClientsViewModel(this, _clientRepository, _currentClient);
        }

        private void ExecuteClientDetailsViewCommand(object obj)
        {
            Title = "Client Details";
            CurrentViewModel = new ClientDetailsViewModel(this, _clientRepository, _currentClient, _currentOrder, _orderRepository, _managerRepository, _tourRepository, _reviewRepository);
        }

        private void ExecuteNewClientViewCommand(object obj)
        {
            Title = "New Client";
            CurrentViewModel = new NewClientViewModel(_clientRepository);
        }

        private void ExecuteManagersViewCommand(object obj)
        {
            Title = "Managers";
            CurrentViewModel = new ManagersViewModel(this, _managerRepository, _currentManager);
        }

        private void ExecuteManagerDetailsCommand(object obj)
        {
            Title = "Manager Details";
            CurrentViewModel = new ManagerDetailsViewModel(this, _managerRepository, _orderRepository, _clientRepository, _tourRepository, _currentManager, _currentOrder);
        }

        private void ExecuteNewManagerViewCommand(object obj)
        {
            Title = "New Manager";
            CurrentViewModel = new NewManagerViewModel(_managerRepository);
        }

        private void ExecuteToursViewCommand(object obj)
        {
            Title = "Tours";
            CurrentViewModel = new ToursViewModel(this, _tourRepository, _currentTour);
        }

        private void ExecuteTourDetailsViewCommand(object obj)
        {
            Title = "Tour Details";
            CurrentViewModel = new TourDetailsViewModel(_currentTour);
        }

        private void ExecuteNewTourViewCommand(object obj)
        {
            Title = "New Tour";
            CurrentViewModel = new NewTourViewModel(_tourRepository);
        }

        private void ExecuteOrdersViewCommand(object obj)
        {
            Title = "Orders";
            CurrentViewModel = new OrdersViewModel(this, _orderRepository, _currentOrder, _managerRepository, _tourRepository, _clientRepository);
        }

        private void ExecuteOrderDetailsViewCommand(object obj)
        {
            Title = "Order Details";
            CurrentViewModel = new OrderDetailsViewModel(this, _orderRepository, _clientRepository, _managerRepository, _tourRepository, _bookingRepository, _currentOrder, _currentBooking, _currentClient, _currentManager, _currentTour);
        }

        private void ExecuteNewOrderViewCommand(object obj)
        {
            Title = "New Order";
            CurrentViewModel = new NewOrderViewModel(this, _orderRepository, _clientRepository, _tourRepository, _managerRepository, _transportTypeRepository);
        }
    }
}

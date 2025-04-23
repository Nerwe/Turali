using System.Windows.Input;
using Turali.Base;
using Turali.Helpers;
using Turali.Repositories;

namespace Turali.ViewModels
{
    public class MainViewModel : BaseViewModel
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

        public BaseViewModel CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel = value;
                OnPropertyChanged(nameof(CurrentViewModel));
            }
        }

        public ICommand DashboardViewCommand { get; }

        public MainViewModel(
            IClientRepository clientRepository,
            IManagerRepository managerRepository,
            ITourRepository tourRepository,
            IOrderRepository orderRepository,
            IReviewRepository reviewRepositor,
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
            _clientRepository = clientRepository;
            _managerRepository = managerRepository;
            _tourRepository = tourRepository;
            _orderRepository = orderRepository;
            _reviewRepository = reviewRepositor;
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

            DashboardViewCommand = new SyncCommand(ExecuteDashboardViewCommand);

            DashboardViewCommand.Execute(null);
        }

        private void ExecuteDashboardViewCommand(object obj)
        {
            CurrentViewModel = new DashboardViewModel(_clientRepository, _managerRepository, _tourRepository, _orderRepository, _reviewRepository, _currentClient, _currentManager, _currentTour, _currentOrder, _currentBooking, _transportTypeRepository, _bookingRepository, _roomRepository, _mealTypeRepository, _hotelRepository);
        }
    }
}

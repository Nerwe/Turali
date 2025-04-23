using System.Collections.ObjectModel;
using System.Windows.Input;
using Turali.Base;
using Turali.Helpers;
using Turali.Repositories;

namespace Turali.ViewModels.Booking
{
    class NewBookingViewModel : BaseViewModel
    {
        private readonly DashboardViewModel _dashboardViewModel;
        private readonly IBookingRepository _bookingRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly IHotelRepository _hotelRepository;
        private readonly IMealTypeRepository _mealTypeRepository;

        private readonly CurrentOrder _currentOrder;

        private Models.Booking _booking = new();
        private Models.Order _order = new();
        private Models.Hotel _hotel = new();

        private ObservableCollection<Models.Client> _clients = [];
        private ObservableCollection<Models.Room> _rooms = [];
        private ObservableCollection<Models.Hotel> _hotels = [];
        private ObservableCollection<Models.MealType> _mealTypes = [];

        public Models.Booking Booking
        {
            get => _booking;
            set
            {
                _booking = value;
                OnPropertyChanged(nameof(Booking));
            }
        }

        public Models.Order Order
        {
            get => _order;
            set
            {
                _order = value;
                OnPropertyChanged(nameof(Order));
            }
        }

        public Models.Hotel Hotel
        {
            get => _hotel;
            set
            {
                _hotel = value;
                OnPropertyChanged(nameof(Hotel));
            }
        }

        public ObservableCollection<Models.Client> Clients
        {
            get => _clients;
            set
            {
                _clients = value;
                OnPropertyChanged(nameof(Clients));
            }
        }

        public ObservableCollection<Models.Room> Rooms
        {
            get => _rooms;
            set
            {
                _rooms = value;
                OnPropertyChanged(nameof(Rooms));
            }
        }

        public ObservableCollection<Models.Hotel> Hotels
        {
            get => _hotels;
            set
            {
                _hotels = value;
                OnPropertyChanged(nameof(Hotels));
            }
        }

        public ObservableCollection<Models.MealType> MealTypes
        {
            get => _mealTypes;
            set
            {
                _mealTypes = value;
                OnPropertyChanged(nameof(MealTypes));
            }
        }

        public ICommand NewBookingCommand { get; }

        public NewBookingViewModel(DashboardViewModel dashboardViewModel,
            IBookingRepository bookingRepository,
            IClientRepository clientRepository,
            IRoomRepository roomRepository,
            IOrderRepository orderRepository,
            IHotelRepository hotelRepository,
            IMealTypeRepository mealTypeRepository,
            CurrentOrder currentOrder)
        {
            _dashboardViewModel = dashboardViewModel;
            _bookingRepository = bookingRepository;
            _clientRepository = clientRepository;
            _roomRepository = roomRepository;
            _hotelRepository = hotelRepository;
            _mealTypeRepository = mealTypeRepository;

            _currentOrder = currentOrder;

            NewBookingCommand = new AsyncCommand(ExecuteNewBookingCommand, CanExecuteNewBookingCommand);

            LoadData();
        }

        private bool CanExecuteNewBookingCommand(object obj)
        {
            return Booking.ClientId > 0 && Booking.RoomId > 0 && Booking.OrderId > 0 && Booking.MealTypeId > 0;
        }

        private async Task ExecuteNewBookingCommand()
        {
            await _bookingRepository.AddAsync(Booking);
            await _bookingRepository.SaveChangesAsync();

            _dashboardViewModel.OrderDetailsViewCommand.Execute(null);
        }

        private async void LoadData()
        {
            if (_currentOrder.Order == null) return;

            Booking.OrderId = _currentOrder.Order.Id;

            Clients = [.. await _clientRepository.GetAllAsync()];
            Hotels = [.. await _hotelRepository.GetAllAsync()];
            Rooms = [.. await _roomRepository.GetAllAsync()];
            MealTypes = [.. await _mealTypeRepository.GetAllAsync()];

            if (Clients.Count > 0)
                Booking.ClientId = Clients[0].Id;
            if (Rooms.Count > 0)
                Booking.RoomId = Rooms[0].Id;
            if (MealTypes.Count > 0)
                Booking.MealTypeId = MealTypes[0].Id;
        }
    }
}

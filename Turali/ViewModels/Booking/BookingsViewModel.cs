using System.Collections.ObjectModel;
using System.Windows.Input;
using Turali.Base;
using Turali.Helpers;
using Turali.Repositories;

namespace Turali.ViewModels.Booking
{
    class BookingsViewModel : BaseViewModel
    {
        private readonly DashboardViewModel _dashboardViewModel;
        private readonly IBookingRepository _bookingRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IMealTypeRepository _mealTypeRepository;
        private readonly IHotelRepository _hotelRepository;

        private readonly CurrentBooking _currentBooking;

        private ObservableCollection<Models.Booking> _bookings = [];

        public ObservableCollection<Models.Booking> Bookings
        {
            get => _bookings;
            set
            {
                _bookings = value;
                OnPropertyChanged(nameof(Bookings));
            }
        }

        public ICommand ShowBookingsCommand { get; }
        public ICommand ShowBookingDetailsViewCommand { get; }

        public BookingsViewModel(DashboardViewModel dashboardViewModel,
            IBookingRepository bookingRepository,
            IRoomRepository roomRepository,
            IClientRepository clientRepository,
            IOrderRepository orderRepository,
            IMealTypeRepository mealTypeRepository,
            IHotelRepository hotelRepository,
            CurrentBooking currentBooking)
        {
            _dashboardViewModel = dashboardViewModel;
            _bookingRepository = bookingRepository;
            _roomRepository = roomRepository;
            _clientRepository = clientRepository;
            _orderRepository = orderRepository;
            _mealTypeRepository = mealTypeRepository;
            _hotelRepository = hotelRepository;

            _currentBooking = currentBooking;

            ShowBookingsCommand = new AsyncCommand(ExecuteShowBookingsCommand);
            ShowBookingDetailsViewCommand = new SyncCommand(ExecuteShowBookingDetailsViewCommand);

            ShowBookingsCommand.Execute(null);
        }

        private void ExecuteShowBookingDetailsViewCommand(object obj)
        {
            if (obj is Models.Booking booking)
            {
                _currentBooking.Booking = booking;
                _dashboardViewModel.BookingDetailsViewCommand.Execute(null);
            }
        }

        private async Task ExecuteShowBookingsCommand()
        {
            var bookings = await _bookingRepository.GetAllAsync();

            foreach (var booking in bookings)
            {
                if (booking.ClientId > 0)
                {
                    var client = await _clientRepository.GetByIdAsync(booking.ClientId);
                    booking.Client = client ?? new Models.Client { FirstName = "Unknown", LastName = "Unknown" };
                }
                if (booking.RoomId > 0)
                {
                    var room = await _roomRepository.GetByIdAsync(booking.RoomId);
                    booking.Room = room ?? new Models.Room { Id = 0 };

                    if (room != null && room.HotelId > 0)
                    {
                        var hotel = await _hotelRepository.GetByIdAsync(room.HotelId);
                        booking.HotelName = hotel?.Name ?? "Unknown";
                    }
                    else
                    {
                        booking.HotelName = "Unknown";
                    }
                }
                if (booking.OrderId > 0)
                {
                    var order = await _orderRepository.GetByIdAsync(booking.OrderId);
                    booking.Order = order ?? new Models.Order { Id = 0 };
                }
                if (booking.MealTypeId.HasValue)
                {
                    var mealType = await _mealTypeRepository.GetByIdAsync(booking.MealTypeId.Value);
                    booking.MealType = mealType;
                }
            }

            Bookings = [.. bookings];
        }
    }
}

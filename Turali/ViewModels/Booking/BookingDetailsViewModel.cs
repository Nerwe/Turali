using System.Windows.Input;
using Turali.Base;
using Turali.Helpers;
using Turali.Repositories;

namespace Turali.ViewModels.Booking
{
    class BookingDetailsViewModel : BaseViewModel
    {
        private readonly CurrentBooking _currentBooking;
        private readonly IClientRepository _clientRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IMealTypeRepository _mealTypeRepository;
        private readonly IHotelRepository _hotelRepository;

        private Models.Booking _booking = new();

        public Models.Booking Booking
        {
            get => _booking;
            set
            {
                _booking = value;
                OnPropertyChanged(nameof(Booking));
            }
        }

        public ICommand ShowBookingDetailsCommand { get; }

        public BookingDetailsViewModel(
            IClientRepository clientRepository,
            IRoomRepository roomRepository,
            IOrderRepository orderRepository,
            IMealTypeRepository mealTypeRepository,
            IHotelRepository hotelRepository,
            CurrentBooking currentBooking)
        {
            _clientRepository = clientRepository;
            _roomRepository = roomRepository;
            _orderRepository = orderRepository;
            _mealTypeRepository = mealTypeRepository;
            _hotelRepository = hotelRepository;

            _currentBooking = currentBooking;

            ShowBookingDetailsCommand = new AsyncCommand(ExecuteShowBookingDetailsCommand);

            ShowBookingDetailsCommand.Execute(null);
        }

        private async Task ExecuteShowBookingDetailsCommand()
        {
            if (_currentBooking.Booking == null) return;
            Booking = _currentBooking.Booking;

            if (Booking.ClientId > 0)
            {
                var client = await _clientRepository.GetByIdAsync(Booking.ClientId);
                Booking.Client = client ?? new Models.Client { FirstName = "Unknown", LastName = "Unknown" };
            }
            if (Booking.RoomId > 0)
            {
                var room = await _roomRepository.GetByIdAsync(Booking.RoomId);
                Booking.Room = room ?? new Models.Room { Id = 0 };

                if (room != null && room.HotelId > 0)
                {
                    var hotel = await _hotelRepository.GetByIdAsync(room.HotelId);
                    Booking.HotelName = hotel?.Name ?? "Unknown";
                }
                else
                {
                    Booking.HotelName = "Unknown";
                }
            }
            if (Booking.OrderId > 0)
            {
                var order = await _orderRepository.GetByIdAsync(Booking.OrderId);
                Booking.Order = order ?? new Models.Order { Id = 0 };
            }
            if (Booking.MealTypeId.HasValue)
            {
                var mealType = await _mealTypeRepository.GetByIdAsync(Booking.MealTypeId.Value);
                Booking.MealType = mealType;
            }
        }
    }
}

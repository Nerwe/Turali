using System.Collections.ObjectModel;
using System.Windows.Input;
using Turali.Base;
using Turali.Repositories;

namespace Turali.ViewModels.Order
{
    public class NewOrderViewModel : BaseViewModel
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IClientRepository _clientRepository;
        private readonly ITourRepository _tourRepository;
        private readonly IManagerRepository _managerRepository;
        private readonly ITransportTypeRepository _transportTypeRepository;
        private readonly DashboardViewModel _dashboardViewModel;

        private Models.Order _order = new();

        private ObservableCollection<Models.Client> _clients = [];
        private ObservableCollection<Models.Tour> _tours = [];
        private ObservableCollection<Models.Manager> _managers = [];
        private ObservableCollection<Models.TransportType> _transportTypes = [];

        public Models.Order Order
        {
            get => _order;
            set
            {
                _order = value;
                OnPropertyChanged(nameof(Order));
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

        public ObservableCollection<Models.Tour> Tours
        {
            get => _tours;
            set
            {
                _tours = value;
                OnPropertyChanged(nameof(Tours));
            }
        }

        public ObservableCollection<Models.Manager> Managers
        {
            get => _managers;
            set
            {
                _managers = value;
                OnPropertyChanged(nameof(Managers));
            }
        }

        public ObservableCollection<Models.TransportType> TransportTypes
        {
            get => _transportTypes;
            set
            {
                _transportTypes = value;
                OnPropertyChanged(nameof(TransportTypes));
            }
        }

        public ICommand NewOrderCommand { get; }

        public NewOrderViewModel(DashboardViewModel dashboardViewModel, IOrderRepository orderRepository, IClientRepository clientRepository, ITourRepository tourRepository, IManagerRepository managerRepository, ITransportTypeRepository transportTypeRepository)
        {
            _dashboardViewModel = dashboardViewModel;
            _orderRepository = orderRepository;
            _clientRepository = clientRepository;
            _tourRepository = tourRepository;
            _managerRepository = managerRepository;
            _transportTypeRepository = transportTypeRepository;

            NewOrderCommand = new AsyncCommand(ExecuteNewOrderCommand, CanNewOrderCommand);

            LoadData();
        }

        private bool CanNewOrderCommand(object obj)
        {
            return Order.ClientId > 0 && Order.ManagerId > 0 && Order.TourId > 0;
        }

        private async Task ExecuteNewOrderCommand()
        {
            var selectedTour = Tours.FirstOrDefault(t => t.Id == Order.TourId);
            if (selectedTour != null)
            {
                Order.TotalPrice = selectedTour.Price;
            }

            await _orderRepository.AddAsync(Order);
            await _orderRepository.SaveChangesAsync();

            _dashboardViewModel.OrdersViewCommand.Execute(null);
        }

        private async void LoadData()
        {
            Clients = [.. await _clientRepository.GetAllAsync()];
            Tours = [.. await _tourRepository.GetAllAsync()];
            Managers = [.. await _managerRepository.GetAllAsync()];
            TransportTypes = [.. await _transportTypeRepository.GetAllAsync()];
            if (Clients.Count > 0)
                Order.ClientId = Clients[0].Id;
            if (Tours.Count > 0)
                Order.TourId = Tours[0].Id;
            if (Managers.Count > 0)
                Order.ManagerId = Managers[0].Id;
            if (TransportTypes.Count > 0)
                Order.TransportTypeId = TransportTypes[0].Id;
        }
    }
}

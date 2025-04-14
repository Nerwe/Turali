using System.Windows.Input;
using Turali.Base;
using Turali.Helpers;
using Turali.Repositories;
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

        private readonly CurrentClient _currentClient = null!;
        private readonly CurrentManager _currentManager = null!;
        private readonly CurrentTour _currentTour = null!;
        private readonly CurrentOrder _currentOrder = null!;

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
        public ICommand ClientDetailViewCommand { get; }
        public ICommand ClientsViewCommand { get; }
        public ICommand NewClientViewCommand { get; }


        //Managers
        public ICommand ManagerDetailsCommand { get; }
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

        public DashboardViewModel(
            IClientRepository clientRepository,
            IManagerRepository managerRepository,
            ITourRepository tourRepository,
            IOrderRepository orderRepository,
            CurrentClient currentClient,
            CurrentManager currentManager,
            CurrentTour currentTour,
            CurrentOrder currentOrder)
        {
            _currentViewModel = new BaseViewModel();

            _clientRepository = clientRepository;
            _managerRepository = managerRepository;
            _tourRepository = tourRepository;
            _orderRepository = orderRepository;

            _currentClient = currentClient;
            _currentManager = currentManager;
            _currentTour = currentTour;
            _currentOrder = currentOrder;

            ClientsViewCommand = new SyncCommand(ExecuteClientsViewCommand);
            ClientDetailViewCommand = new SyncCommand(ExecuteClientDetailViewCommand);
            NewClientViewCommand = new SyncCommand(ExecuteNewClientViewCommand);

            ManagersViewCommand = new SyncCommand(ExecuteManagersViewCommand);
            ManagerDetailsCommand = new SyncCommand(ExecuteManagerDetailsCommand);
            NewManagerViewCommand = new SyncCommand(ExecuteNewManagerViewCommand);

            ToursViewCommand = new SyncCommand(ExecuteToursViewCommand);
            TourDetailsViewCommand = new SyncCommand(ExecuteTourDetailsViewCommand);
            NewTourViewCommand = new SyncCommand(ExecuteNewTourViewCommand);

            OrdersViewCommand = new SyncCommand(ExecuteOrdersViewCommand);
            OrderDetailsViewCommand = new SyncCommand(ExecuteOrderDetailsViewCommand);
            NewOrderViewCommand = new SyncCommand(ExecuteNewOrderViewCommand);

            ClientsViewCommand.Execute(null);
        }

        private void ExecuteClientsViewCommand(object obj)
        {
            Title = "Clients";
            CurrentViewModel = new ClientsViewModel(this, _clientRepository, _currentClient);
        }

        private void ExecuteClientDetailViewCommand(object obj)
        {
            Title = "Client Details";
            CurrentViewModel = new ClientDetailsViewModel(_clientRepository, _currentClient);
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
            CurrentViewModel = new ManagerDetailsViewModel(_managerRepository, _currentManager);
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
            CurrentViewModel = new TourDetailsViewModel(_tourRepository, _currentTour);
        }

        private void ExecuteNewTourViewCommand(object obj)
        {
            Title = "New Tour";
            CurrentViewModel = new NewTourViewModel(_tourRepository);
        }

        private void ExecuteOrdersViewCommand(object obj)
        {
            Title = "Orders";
            CurrentViewModel = new OrdersViewModel(this, _orderRepository, _currentOrder);
        }

        private void ExecuteOrderDetailsViewCommand(object obj)
        {
            Title = "Order Details";
            CurrentViewModel = new OrderDetailsViewModel(_orderRepository, _currentOrder);
        }

        private void ExecuteNewOrderViewCommand(object obj)
        {
            Title = "New Order";
            CurrentViewModel = new NewOrderViewModel(_orderRepository);
        }
    }
}

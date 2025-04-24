using System.Collections.ObjectModel;
using System.Windows.Input;
using Turali.Base;
using Turali.Helpers;
using Turali.Repositories;

namespace Turali.ViewModels.Manager
{
    public class ManagerDetailsViewModel : BaseViewModel
    {
        private readonly DashboardViewModel _dashboardViewModel;
        private readonly IManagerRepository _managerRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IClientRepository _clientRepository;
        private readonly ITourRepository _tourRepository;

        private readonly CurrentManager _currentManager;
        private readonly CurrentOrder _currentOrder;
        private Models.Manager _manager = new();

        private int _orderCount;

        private ObservableCollection<Models.Order> _orders = [];

        public int OrderCount
        {
            get => _orderCount;
            set
            {
                _orderCount = value;
                OnPropertyChanged(nameof(OrderCount));
            }
        }

        public ObservableCollection<Models.Order> Orders
        {
            get => _orders;
            set
            {
                _orders = value;
                OnPropertyChanged(nameof(Orders));
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

        public ICommand ShowManagerDetailsCommand { get; }
        public ICommand SaveChangesCommand { get; }
        public ICommand ShowManagerOrdersCommand { get; }

        public ICommand ShowOrderDetailsViewCommand { get; }

        public ManagerDetailsViewModel(
            DashboardViewModel dashboardViewModel,
            IManagerRepository managerRepository,
            IOrderRepository orderRepository,
            IClientRepository clientRepository,
            ITourRepository tourRepository,
            CurrentManager currentManager,
            CurrentOrder currentOrder)
        {
            _dashboardViewModel = dashboardViewModel;
            _managerRepository = managerRepository;
            _currentManager = currentManager;
            _currentOrder = currentOrder;
            _orderRepository = orderRepository;
            _clientRepository = clientRepository;
            _tourRepository = tourRepository;

            ShowManagerDetailsCommand = new SyncCommand(ExecuteShowClientDetailsCommand);
            SaveChangesCommand = new SyncCommand(ExecuteSaveChangesCommand);
            ShowManagerOrdersCommand = new AsyncCommand(ExecuteShowMangerOrdersCommand);
            ShowOrderDetailsViewCommand = new SyncCommand(ExecuteShowOrderDetailsViewCommand);


            ShowManagerDetailsCommand.Execute(new object());
            ShowManagerOrdersCommand.Execute(new object());
        }

        private void ExecuteShowOrderDetailsViewCommand(object obj)
        {
            if (obj is Models.Order order)
            {
                _currentOrder.Order = order;
                _dashboardViewModel.OrderDetailsViewCommand.Execute(null);
            }
        }

        private async Task ExecuteShowMangerOrdersCommand()
        {
            if (_currentManager.Manager == null) return;

            var orders = await _orderRepository.GetOrdersByManagerIdAsync(_currentManager.Manager.Id);

            foreach (var order in orders)
            {
                if (order.ClientId > 0)
                {
                    var client = await _clientRepository.GetByIdAsync(order.ClientId);
                    order.Client = client ?? new Models.Client { FirstName = "Unknown", LastName = "Unknown" };
                }

                if (order.ManagerId > 0)
                {
                    var manager = await _managerRepository.GetByIdAsync(order.ManagerId);
                    order.Manager = manager ?? new Models.Manager { FirstName = "Unknown", LastName = "Unknown" };
                }

                if (order.TourId > 0)
                {
                    var tour = await _tourRepository.GetByIdAsync(order.TourId);
                    order.Tour = tour ?? new Models.Tour { Name = "Unknown" };
                }
            }

            Orders = [.. orders];
            OrderCount = orders.Count();
        }

        private void ExecuteSaveChangesCommand(object obj)
        {
            _managerRepository.Update(Manager);
        }

        private void ExecuteShowClientDetailsCommand(object obj)
        {
            if (_currentManager.Manager == null) return;

            Manager = _currentManager.Manager;
        }
    }
}

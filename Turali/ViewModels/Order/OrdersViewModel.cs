using System.Collections.ObjectModel;
using System.Windows.Input;
using Turali.Base;
using Turali.Helpers;
using Turali.Repositories;

namespace Turali.ViewModels.Order
{
    public class OrdersViewModel : BaseViewModel
    {
        private readonly DashboardViewModel _dashboardViewModel;
        private readonly IOrderRepository _orderRepository;
        private readonly IManagerRepository _managerRepository;
        private readonly ITourRepository _tourRepository;
        private readonly CurrentOrder _currentOrder;
        private readonly IClientRepository _clientRepository;

        private ObservableCollection<Models.Order> _orders = [];

        public ObservableCollection<Models.Order> Orders
        {
            get => _orders;
            set
            {
                _orders = value;
                OnPropertyChanged(nameof(Orders));
            }
        }

        public ICommand ShowOrdersCommand { get; }
        public ICommand ShowOrderDetailsViewCommand { get; }
        public ICommand ShowNewOrderViewCommand { get; }


        public OrdersViewModel(DashboardViewModel dashboardViewModel, IOrderRepository orderRepository, CurrentOrder currentOrder, IManagerRepository managerRepository, ITourRepository tourRepository, IClientRepository clientRepository)
        {
            _dashboardViewModel = dashboardViewModel;
            _orderRepository = orderRepository;
            _currentOrder = currentOrder;
            _managerRepository = managerRepository;
            _tourRepository = tourRepository;
            _clientRepository = clientRepository;

            ShowOrdersCommand = new AsyncCommand(ExecuteShowOrdersCommand);
            ShowOrderDetailsViewCommand = new SyncCommand(ExecuteShowOrderDetailsViewCommand);
            ShowNewOrderViewCommand = new SyncCommand(ExecuteShowNewOrderViewCommand);

            ShowOrdersCommand.Execute(null);
        }

        private void ExecuteShowNewOrderViewCommand(object obj)
        {
            _dashboardViewModel.NewOrderViewCommand.Execute(null);
        }

        private async Task ExecuteShowOrdersCommand()
        {
            var orders = await _orderRepository.GetAllAsync();

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
        }

        private void ExecuteShowOrderDetailsViewCommand(object obj)
        {
            if (obj is Models.Order order)
            {
                _currentOrder.Order = order;
                _dashboardViewModel.CurrentViewModel = new OrderDetailsViewModel(_orderRepository, _currentOrder);
            }
        }
    }
}

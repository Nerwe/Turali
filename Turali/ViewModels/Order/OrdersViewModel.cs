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
        private readonly CurrentOrder _currentOrder;

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

        public OrdersViewModel(DashboardViewModel dashboardViewModel, IOrderRepository orderRepository, CurrentOrder currentOrder)
        {
            _dashboardViewModel = dashboardViewModel;
            _orderRepository = orderRepository;
            _currentOrder = currentOrder;

            ShowOrdersCommand = new AsyncCommand(ExecuteShowOrdersCommand);
            ShowOrderDetailsViewCommand = new SyncCommand(ExecuteShowOrderDetailsViewCommand);

            ShowOrdersCommand.Execute(null);
        }

        private async Task ExecuteShowOrdersCommand()
        {
            Orders = [.. await _orderRepository.GetAllAsync()];
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

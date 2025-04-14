using System.Windows.Input;
using Turali.Base;
using Turali.Helpers;
using Turali.Repositories;

namespace Turali.ViewModels.Order
{
    public class OrderDetailsViewModel : BaseViewModel
    {
        private readonly IOrderRepository _orderRepository;

        private readonly CurrentOrder _currentOrder;
        private Models.Order _order = new();

        public Models.Order Order
        {
            get => _order;
            set
            {
                _order = value;
                OnPropertyChanged(nameof(Order));
            }
        }

        public ICommand SaveChangesCommand { get; }
        public ICommand ShowOrderDetailsCommand { get; }

        public OrderDetailsViewModel(IOrderRepository orderRepository, CurrentOrder currentOrder)
        {
            _orderRepository = orderRepository;
            _currentOrder = currentOrder;

            SaveChangesCommand = new SyncCommand(ExecuteSaveChangesCommand, CanExecuteSaveChangesCommand);
            ShowOrderDetailsCommand = new SyncCommand(ExecuteShowOrderDetailsCommand);

            ShowOrderDetailsCommand.Execute(new object());
        }

        private bool CanExecuteSaveChangesCommand(object obj)
        {
            throw new NotImplementedException();
        }

        private void ExecuteShowOrderDetailsCommand(object obj)
        {
            if (_currentOrder.Order == null) return;

            Order = _currentOrder.Order;
        }

        private void ExecuteSaveChangesCommand(object obj)
        {
            _orderRepository.Update(Order);
        }
    }
}

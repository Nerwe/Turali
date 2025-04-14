using System.Windows.Input;
using Turali.Base;
using Turali.Repositories;

namespace Turali.ViewModels.Order
{
    public class NewOrderViewModel : BaseViewModel
    {
        private readonly IOrderRepository _orderRepository;
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

        public NewOrderViewModel(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
            SaveChangesCommand = new AsyncCommand(ExecuteSaveChangesCommand, CanExecuteSaveChangesCommand);
        }

        private bool CanExecuteSaveChangesCommand(object obj)
        {
            return true;
        }

        private async Task ExecuteSaveChangesCommand()
        {
            await _orderRepository.AddAsync(Order);
        }
    }
}

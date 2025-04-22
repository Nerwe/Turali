using System.Windows.Input;
using Turali.Base;
using Turali.Helpers;
using Turali.Repositories;

namespace Turali.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private BaseViewModel _currentViewModel = null!;

        private readonly IClientRepository _clientRepository = null!;
        private readonly IManagerRepository _managerRepository = null!;
        private readonly ITourRepository _tourRepository = null!;
        private readonly IOrderRepository _orderRepository = null!;
        private readonly IReviewRepository _reviewRepository = null!;
        private readonly ITransportTypeRepository _transportTypeRepository = null!;

        private readonly CurrentClient _currentClient = null!;
        private readonly CurrentManager _currentManager = null!;
        private readonly CurrentTour _currentTour = null!;
        private readonly CurrentOrder _currentOrder = null!;

        public BaseViewModel CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel = value;
                OnPropertyChanged(nameof(CurrentViewModel));
            }
        }

        public ICommand DashboardViewCommand { get; }

        public MainViewModel(
            IClientRepository clientRepository,
            IManagerRepository managerRepository,
            ITourRepository tourRepository,
            IOrderRepository orderRepository,
            IReviewRepository reviewRepositor,
            CurrentClient currentClient,
            CurrentManager currentManager,
            CurrentTour currentTour,
            CurrentOrder currentOrder,
            ITransportTypeRepository transportTypeRepository)
        {
            _clientRepository = clientRepository;
            _managerRepository = managerRepository;
            _tourRepository = tourRepository;
            _orderRepository = orderRepository;
            _reviewRepository = reviewRepositor;
            _transportTypeRepository = transportTypeRepository;

            _currentClient = currentClient;
            _currentManager = currentManager;
            _currentTour = currentTour;
            _currentOrder = currentOrder;

            DashboardViewCommand = new SyncCommand(ExecuteDashboardViewCommand);

            DashboardViewCommand.Execute(null);
        }

        private void ExecuteDashboardViewCommand(object obj)
        {
            CurrentViewModel = new DashboardViewModel(_clientRepository, _managerRepository, _tourRepository, _orderRepository, _reviewRepository, _currentClient, _currentManager, _currentTour, _currentOrder, _transportTypeRepository);
        }
    }
}

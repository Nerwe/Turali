using System.Collections.ObjectModel;
using System.Windows.Input;
using Turali.Base;
using Turali.Helpers;
using Turali.Repositories;

namespace Turali.ViewModels.Client
{
    public class ClientDetailsViewModel : BaseViewModel
    {
        private readonly IClientRepository _clientRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IManagerRepository _managerRepository;
        private readonly ITourRepository _tourRepository;
        private readonly IReviewRepository _reviewRepository;

        private CurrentClient _currentClient;
        private Models.Client _client = new();

        private int _orderCount;
        private int _reviewCount;

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

        public int ReviewCount
        {
            get => _reviewCount;
            set
            {
                _reviewCount = value;
                OnPropertyChanged(nameof(ReviewCount));
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

        public Models.Client Client
        {
            get => _client;
            set
            {
                _client = value;
                OnPropertyChanged(nameof(Client));
            }
        }

        public ICommand ShowClientDetailsCommand { get; }
        public ICommand SaveChangesCommand { get; }
        public ICommand ShowClientOrdersCommand { get; }
        public ICommand ShowClientReviewsCommand { get; }

        public ClientDetailsViewModel(
            IClientRepository clientRepository,
            CurrentClient currentClient,
            IOrderRepository orderRepository,
            IManagerRepository managerRepository,
            ITourRepository tourRepository,
            IReviewRepository reviewRepository)
        {
            _clientRepository = clientRepository;
            _orderRepository = orderRepository;
            _currentClient = currentClient;
            _managerRepository = managerRepository;
            _tourRepository = tourRepository;
            _reviewRepository = reviewRepository;

            ShowClientDetailsCommand = new SyncCommand(ExecuteShowClientDetailsCommand);
            SaveChangesCommand = new SyncCommand(ExecuteSaveChangesCommand);
            ShowClientOrdersCommand = new AsyncCommand(ExecuteShowClientOrdersCommand);
            ShowClientReviewsCommand = new AsyncCommand(ExecuteShowClientReviewsCommand);

            Initialize();
        }

        private async void Initialize()
        {
            ExecuteShowClientDetailsCommand(null);
            await ExecuteShowClientOrdersCommand();
            await ExecuteShowClientReviewsCommand();
        }

        private async Task ExecuteShowClientReviewsCommand()
        {
            if (_currentClient.Client == null) return;

            var reviews = await _reviewRepository.GetReviewsByClientIdAsync(_currentClient.Client.Id);
            ReviewCount = reviews.Count();
        }

        private async Task ExecuteShowClientOrdersCommand()
        {
            if (_currentClient.Client == null) return;

            var orders = await _orderRepository.GetOrdersByClientIdAsync(_currentClient.Client.Id);

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

            Orders = new ObservableCollection<Models.Order>(orders);
            OrderCount = orders.Count();
        }

        private void ExecuteSaveChangesCommand(object obj)
        {
            _clientRepository.Update(Client);
        }

        private void ExecuteShowClientDetailsCommand(object obj)
        {
            if (_currentClient.Client == null) return;

            Client = _currentClient.Client;
        }
    }
}

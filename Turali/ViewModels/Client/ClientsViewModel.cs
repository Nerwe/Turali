using System.Collections.ObjectModel;
using System.Linq.Expressions;
using System.Windows.Input;
using Turali.Base;
using Turali.Helpers;
using Turali.Repositories;

namespace Turali.ViewModels.Client
{
    public class ClientsViewModel : BaseViewModel
    {
        private readonly DashboardViewModel _dashboardViewModel;
        private readonly IClientRepository _clientRepository;
        private readonly IReviewRepository _reviewRepository;
        private readonly CurrentClient _currentClient;

        private string _searchQuery = string.Empty;
        private ObservableCollection<Models.Client> _clients = [];

        public string SearchQuery
        {
            get => _searchQuery;
            set
            {
                _searchQuery = value;
                OnPropertyChanged(nameof(SearchQuery));
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

        public ICommand ShowClientsCommand { get; }
        public ICommand ShowClientDetailsViewCommand { get; }
        public ICommand SearchCommand { get; }
        public ICommand ClearSearchCommand { get; }

        public ClientsViewModel(DashboardViewModel dashboardViewModel, IClientRepository clientRepository, IReviewRepository reviewRepository, CurrentClient currentClient)
        {
            _dashboardViewModel = dashboardViewModel;
            _clientRepository = clientRepository;
            _currentClient = currentClient;
            _reviewRepository = reviewRepository;

            ShowClientsCommand = new AsyncCommand(ExecuteShowClientsCommand);
            ShowClientDetailsViewCommand = new SyncCommand(ExecuteShowClientDetailsViewCommand);
            SearchCommand = new AsyncCommand(ExecuteSearchCommand);
            ClearSearchCommand = new AsyncCommand(ExecuteClearSearchCommand);

            ShowClientsCommand.Execute(null);
        }

        private async Task ExecuteShowClientsCommand()
        {
            Clients = [.. await _clientRepository.GetAllAsync()];
        }

        private void ExecuteShowClientDetailsViewCommand(object obj)
        {
            if (obj is Models.Client client)
            {
                _currentClient.Client = client;
                _dashboardViewModel.ClientDetailsViewCommand.Execute(null);
            }
        }

        private async Task ExecuteSearchCommand()
        {
            if (string.IsNullOrWhiteSpace(SearchQuery))
            {
                Clients = [.. await _clientRepository.GetAllAsync()];
            }
            else
            {
                Expression<Func<Models.Client, bool>> filter = c =>
                    (c.FirstName != null && c.FirstName.Contains(SearchQuery)) ||
                    (c.LastName != null && c.LastName.Contains(SearchQuery)) ||
                    (c.PassportNumber != null && c.PassportNumber.Contains(SearchQuery)) ||
                    (c.Phone != null && c.Phone.Contains(SearchQuery)) ||
                    (c.Email != null && c.Email.Contains(SearchQuery));

                var filteredClients = await _clientRepository.FindAsync(filter);
                Clients = [.. filteredClients];
            }
        }

        private async Task ExecuteClearSearchCommand()
        {
            SearchQuery = string.Empty;
            Clients = [.. await _clientRepository.GetAllAsync()];
        }
    }
}

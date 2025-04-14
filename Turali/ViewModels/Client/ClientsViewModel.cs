using System.Collections.ObjectModel;
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
        private readonly CurrentClient _currentClient;

        private ObservableCollection<Models.Client> _clients = [];

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

        public ClientsViewModel(DashboardViewModel dashboardViewModel, IClientRepository clientRepository, CurrentClient currentClient)
        {
            _dashboardViewModel = dashboardViewModel;
            _clientRepository = clientRepository;
            _currentClient = currentClient;

            ShowClientsCommand = new AsyncCommand(ExecuteShowClientsCommand);
            ShowClientDetailsViewCommand = new SyncCommand(ExecuteShowClientDetailsViewCommand);

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
                _dashboardViewModel.CurrentViewModel = new ClientDetailsViewModel(_clientRepository, _currentClient);
            }
        }
    }
}

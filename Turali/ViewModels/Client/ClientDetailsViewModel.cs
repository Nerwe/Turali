using System.Windows.Input;
using Turali.Base;
using Turali.Helpers;
using Turali.Repositories;

namespace Turali.ViewModels.Client
{
    public class ClientDetailsViewModel : BaseViewModel
    {
        private readonly IClientRepository _clientRepository;

        private CurrentClient _currentClient;
        private Models.Client _client = new();

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

        public ClientDetailsViewModel(IClientRepository clientRepository, CurrentClient currentClient)
        {
            _clientRepository = clientRepository;
            _currentClient = currentClient;

            ShowClientDetailsCommand = new SyncCommand(ExecuteShowClientDetailsCommand);
            SaveChangesCommand = new SyncCommand(ExecuteSaveChangesCommand);

            ExecuteShowClientDetailsCommand(new object());
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

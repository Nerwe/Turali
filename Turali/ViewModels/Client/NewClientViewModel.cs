using System.Windows.Input;
using Turali.Base;
using Turali.Repositories;

namespace Turali.ViewModels.Client
{
    public class NewClientViewModel : BaseViewModel
    {
        private readonly IClientRepository _clientRepository;

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

        public ICommand SaveChangesCommand { get; }

        public NewClientViewModel(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;

            SaveChangesCommand = new AsyncCommand(ExecuteSaveChangesCommand, CanExecuteSaveChangesCommand);
        }

        private bool CanExecuteSaveChangesCommand(object obj)
        {
            return true;
        }

        private async Task ExecuteSaveChangesCommand()
        {
            await _clientRepository.AddAsync(Client);
        }
    }
}

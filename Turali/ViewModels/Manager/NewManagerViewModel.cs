using System.Windows.Input;
using Turali.Base;
using Turali.Repositories;

namespace Turali.ViewModels.Manager
{
    public class NewManagerViewModel : BaseViewModel
    {
        private readonly IManagerRepository _managerRepository;

        private Models.Manager _manager = new();

        public Models.Manager Manager
        {
            get => _manager;
            set
            {
                _manager = value;
                OnPropertyChanged(nameof(Manager));
            }
        }

        public ICommand SaveChangesCommand { get; }

        public NewManagerViewModel(IManagerRepository managerRepository)
        {
            _managerRepository = managerRepository;
            SaveChangesCommand = new AsyncCommand(ExecuteSaveChangesCommand, CanExecuteSaveChangesCommand);
        }

        private bool CanExecuteSaveChangesCommand(object obj)
        {
            throw new NotImplementedException();
        }

        private async Task ExecuteSaveChangesCommand()
        {
            await _managerRepository.AddAsync(Manager);
        }
    }
}

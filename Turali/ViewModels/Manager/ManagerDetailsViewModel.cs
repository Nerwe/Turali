using System.Windows.Input;
using Turali.Base;
using Turali.Helpers;
using Turali.Repositories;

namespace Turali.ViewModels.Manager
{
    public class ManagerDetailsViewModel : BaseViewModel
    {
        private readonly IManagerRepository _managerRepository;

        private readonly CurrentManager _currentManager;
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

        public ICommand ShowManagerDetailsCommand { get; }
        public ICommand SaveChangesCommand { get; }

        public ManagerDetailsViewModel(IManagerRepository managerRepository, CurrentManager currentManager)
        {
            _managerRepository = managerRepository;
            _currentManager = currentManager;

            ShowManagerDetailsCommand = new SyncCommand(ExecuteShowClientDetailsCommand);
            SaveChangesCommand = new SyncCommand(ExecuteSaveChangesCommand, CanExecuteSaveChangesCommand);

            ShowManagerDetailsCommand.Execute(new object());
        }

        private bool CanExecuteSaveChangesCommand(object obj)
        {
            throw new NotImplementedException();
        }

        private void ExecuteSaveChangesCommand(object obj)
        {
            _managerRepository.Update(Manager);
        }

        private void ExecuteShowClientDetailsCommand(object obj)
        {
            if (_currentManager.Manager == null) return;

            Manager = _currentManager.Manager;
        }
    }
}

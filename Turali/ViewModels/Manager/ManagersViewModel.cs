using System.Collections.ObjectModel;
using System.Windows.Input;
using Turali.Base;
using Turali.Helpers;
using Turali.Repositories;

namespace Turali.ViewModels.Manager
{
    public class ManagersViewModel : BaseViewModel
    {
        private readonly DashboardViewModel _dashboardViewModel;
        private readonly IManagerRepository _managerRepository;
        private readonly CurrentManager _currentManager;

        private ObservableCollection<Models.Manager> _managers = [];

        public ObservableCollection<Models.Manager> Managers
        {
            get => _managers;
            set
            {
                _managers = value;
                OnPropertyChanged(nameof(Managers));
            }
        }

        public ICommand ShowManagersCommand { get; }
        public ICommand ShowManagerDetailsViewCommand { get; }

        public ManagersViewModel(DashboardViewModel dashboardViewModel, IManagerRepository managerRepository, CurrentManager currentManager)
        {
            _dashboardViewModel = dashboardViewModel;
            _managerRepository = managerRepository;
            _currentManager = currentManager;

            ShowManagersCommand = new AsyncCommand(ExecuteShowManagersCommand);
            ShowManagerDetailsViewCommand = new SyncCommand(ExecuteShowManagerDetailsViewCommand);

            ShowManagersCommand.Execute(null);
        }

        private async Task ExecuteShowManagersCommand()
        {
            Managers = [.. await _managerRepository.GetAllAsync()];
        }

        private void ExecuteShowManagerDetailsViewCommand(object obj)
        {
            if (obj is Models.Manager manager)
            {
                _currentManager.Manager = manager;
                _dashboardViewModel.CurrentViewModel = new ManagerDetailsViewModel(_managerRepository, _currentManager);
            }
        }
    }
}

using System.Collections.ObjectModel;
using System.Linq.Expressions;
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

        private string _searchQuery = string.Empty;
        private ObservableCollection<Models.Manager> _managers = [];

        public string SearchQuery
        {
            get => _searchQuery;
            set
            {
                _searchQuery = value;
                OnPropertyChanged(nameof(SearchQuery));
            }
        }

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
        public ICommand SearchCommand { get; }
        public ICommand ClearSearchCommand { get; }

        public ManagersViewModel(DashboardViewModel dashboardViewModel, IManagerRepository managerRepository, CurrentManager currentManager)
        {
            _dashboardViewModel = dashboardViewModel;
            _managerRepository = managerRepository;
            _currentManager = currentManager;

            ShowManagersCommand = new AsyncCommand(ExecuteShowManagersCommand);
            ShowManagerDetailsViewCommand = new SyncCommand(ExecuteShowManagerDetailsViewCommand);
            SearchCommand = new AsyncCommand(ExecuteSearchCommand);
            ClearSearchCommand = new AsyncCommand(ExecuteClearSearchCommand);

            ShowManagersCommand.Execute(null);
        }

        private async Task ExecuteShowManagersCommand()
        {
            var managers = await _managerRepository.GetAllAsync();

            foreach (var manager in managers)
            {
                manager.AverageRating = await _managerRepository.GetAverageRatingAsync(manager.Id);
            }

            Managers = [.. managers];
        }

        private void ExecuteShowManagerDetailsViewCommand(object obj)
        {
            if (obj is Models.Manager manager)
            {
                _currentManager.Manager = manager;
                _dashboardViewModel.ManagerDetailsViewCommand.Execute(null);
            }
        }

        private async Task ExecuteSearchCommand()
        {
            if (string.IsNullOrWhiteSpace(SearchQuery))
            {
                Managers = [.. await _managerRepository.GetAllAsync()];
            }
            else
            {
                Expression<Func<Models.Manager, bool>> filter = c =>
                    (c.FirstName != null && c.FirstName.Contains(SearchQuery)) ||
                    (c.LastName != null && c.LastName.Contains(SearchQuery)) ||
                    (c.Phone != null && c.Phone.Contains(SearchQuery)) ||
                    (c.Email != null && c.Email.Contains(SearchQuery));

                var filteredManagers = await _managerRepository.FindAsync(filter);
                Managers = [.. filteredManagers];
            }
        }


        private async Task ExecuteClearSearchCommand()
        {
            SearchQuery = string.Empty;
            Managers = [.. await _managerRepository.GetAllAsync()];
        }
    }
}

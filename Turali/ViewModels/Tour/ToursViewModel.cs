using System.Collections.ObjectModel;
using System.Linq.Expressions;
using System.Windows.Input;
using Turali.Base;
using Turali.Helpers;
using Turali.Repositories;

namespace Turali.ViewModels.Tour
{
    public class ToursViewModel : BaseViewModel
    {
        private readonly DashboardViewModel _dashboardViewModel;
        private readonly ITourRepository _tourRepository;
        private readonly CurrentTour _currentTour;

        private string _searchQuery = string.Empty;
        private ObservableCollection<Models.Tour> _tours = [];

        public string SearchQuery
        {
            get => _searchQuery;
            set
            {
                _searchQuery = value;
                OnPropertyChanged(nameof(SearchQuery));
            }
        }

        public ObservableCollection<Models.Tour> Tours
        {
            get => _tours;
            set
            {
                _tours = value;
                OnPropertyChanged(nameof(Tours));
            }
        }

        public ICommand ShowToursCommand { get; }
        public ICommand ShowTourDetailsViewCommand { get; }
        public ICommand SearchCommand { get; }
        public ICommand ClearSearchCommand { get; }

        public ToursViewModel(DashboardViewModel dashboardViewModel, ITourRepository tourRepository, CurrentTour currentTour)
        {
            _dashboardViewModel = dashboardViewModel;
            _tourRepository = tourRepository;
            _currentTour = currentTour;

            ShowToursCommand = new AsyncCommand(ExecuteShowToursCommand);
            ShowTourDetailsViewCommand = new SyncCommand(ExecuteShowTourDetailsViewCommand);
            SearchCommand = new AsyncCommand(ExecuteSearchCommand);
            ClearSearchCommand = new AsyncCommand(ExecuteClearSearchCommand);

            ShowToursCommand.Execute(null);
        }

        private async Task ExecuteShowToursCommand()
        {
            Tours = [.. await _tourRepository.GetAllAsync()];
        }

        private void ExecuteShowTourDetailsViewCommand(object obj)
        {
            if (obj is Models.Tour tour)
            {
                _currentTour.Tour = tour;
                _dashboardViewModel.CurrentViewModel = new TourDetailsViewModel(_tourRepository, _currentTour);
            }
        }

        private async Task ExecuteSearchCommand()
        {
            if (string.IsNullOrWhiteSpace(SearchQuery))
            {
                Tours = [.. await _tourRepository.GetAllAsync()];
            }
            else
            {
                Expression<Func<Models.Tour, bool>> filter = c =>
                    (c.Name != null && c.Name.Contains(SearchQuery)) ||
                    (c.Description != null && c.Description.Contains(SearchQuery));

                var filteredTours = await _tourRepository.FindAsync(filter);
                Tours = [.. filteredTours];
            }
        }


        private async Task ExecuteClearSearchCommand()
        {
            SearchQuery = string.Empty;
            Tours = [.. await _tourRepository.GetAllAsync()];
        }
    }
}

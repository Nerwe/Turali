using System.Collections.ObjectModel;
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

        private ObservableCollection<Models.Tour> _tours = [];

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

        public ToursViewModel(DashboardViewModel dashboardViewModel, ITourRepository tourRepository, CurrentTour currentTour)
        {
            _dashboardViewModel = dashboardViewModel;
            _tourRepository = tourRepository;
            _currentTour = currentTour;

            ShowToursCommand = new AsyncCommand(ExecuteShowToursCommand);
            ShowTourDetailsViewCommand = new SyncCommand(ExecuteShowTourDetailsViewCommand);

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
    }
}

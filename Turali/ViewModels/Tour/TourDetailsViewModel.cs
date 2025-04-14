using System.Windows.Input;
using Turali.Base;
using Turali.Helpers;
using Turali.Repositories;

namespace Turali.ViewModels.Tour
{
    public class TourDetailsViewModel : BaseViewModel
    {
        private readonly ITourRepository _tourRepository;
        private readonly CurrentTour _currentTour;
        private Models.Tour _tour = new();

        public Models.Tour Tour
        {
            get => _tour;
            set
            {
                _tour = value;
                OnPropertyChanged(nameof(Tour));
            }
        }

        public ICommand SaveChangesCommand { get; }
        public ICommand ShowTourDetailsCommand { get; }

        public TourDetailsViewModel(ITourRepository tourRepository, CurrentTour currentTour)
        {
            _tourRepository = tourRepository;
            _currentTour = currentTour;
            ShowTourDetailsCommand = new SyncCommand(ExecuteShowTourDetailsCommand);
            SaveChangesCommand = new SyncCommand(ExecuteSaveChangesCommand, CanExecuteSaveChangesCommand);
            ShowTourDetailsCommand.Execute(null);
        }

        private bool CanExecuteSaveChangesCommand(object obj)
        {
            return true;
        }

        private void ExecuteShowTourDetailsCommand(object obj)
        {
            if (_currentTour.Tour == null) return;
            Tour = _currentTour.Tour;
        }

        private void ExecuteSaveChangesCommand(object obj)
        {
            _tourRepository.Update(Tour);
        }
    }
}

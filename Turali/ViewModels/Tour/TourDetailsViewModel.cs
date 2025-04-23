using System.Windows.Input;
using Turali.Base;
using Turali.Helpers;

namespace Turali.ViewModels.Tour
{
    public class TourDetailsViewModel : BaseViewModel
    {
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

        public ICommand ShowTourDetailsCommand { get; }

        public TourDetailsViewModel(CurrentTour currentTour)
        {
            _currentTour = currentTour;
            ShowTourDetailsCommand = new SyncCommand(ExecuteShowTourDetailsCommand);
            ShowTourDetailsCommand.Execute(null);
        }

        private void ExecuteShowTourDetailsCommand(object obj)
        {
            if (_currentTour.Tour == null) return;
            Tour = _currentTour.Tour;
        }
    }
}

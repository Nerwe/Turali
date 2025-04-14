using System.Windows.Input;
using Turali.Base;
using Turali.Repositories;

namespace Turali.ViewModels.Tour
{
    public class NewTourViewModel : BaseViewModel
    {
        private readonly ITourRepository _tourRepository;
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

        public NewTourViewModel(ITourRepository tourRepository)
        {
            _tourRepository = tourRepository;
            SaveChangesCommand = new AsyncCommand(ExecuteSaveChangesCommand, CanExecuteSaveChangesCommand);
        }

        private bool CanExecuteSaveChangesCommand(object obj)
        {
            return true;
        }

        private async Task ExecuteSaveChangesCommand()
        {
            await _tourRepository.AddAsync(Tour);
        }
    }
}

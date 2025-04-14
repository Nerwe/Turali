using System.Windows.Input;

namespace Turali.Base
{
    internal class AsyncCommand : ICommand
    {
        private readonly Func<Task> _executeAsync;
        private readonly Predicate<object>? _canExecute;

        public AsyncCommand(Func<Task> executeAsync)
        {
            _executeAsync = executeAsync ?? throw new ArgumentNullException(nameof(executeAsync));
            _canExecute = null;
        }

        public AsyncCommand(Func<Task> executeAsync, Predicate<object>? canExecute)
        {
            _executeAsync = executeAsync ?? throw new ArgumentNullException(nameof(executeAsync));
            _canExecute = canExecute;
        }

        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object? parameter) => _canExecute?.Invoke(parameter!) ?? true;

        public async void Execute(object? parameter)
        {
            await _executeAsync();
        }
    }
}

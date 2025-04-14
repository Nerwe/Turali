using System.Windows.Input;

namespace Turali.Base
{
    internal class SyncCommand : ICommand
    {
        //Fields
        private readonly Action<object> _executeAction;
        private readonly Predicate<object>? _canExucuteAction;

        //Constructors
        public SyncCommand(Action<object> executeAction)
        {
            _executeAction = executeAction ?? throw new ArgumentNullException(nameof(executeAction));
            _canExucuteAction = null;
        }

        public SyncCommand(Action<object> executeAction, Predicate<object>? canExucuteAction)
        {
            _executeAction = executeAction ?? throw new ArgumentNullException(nameof(executeAction));
            _canExucuteAction = canExucuteAction;
        }

        //Events
        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        //Methods
        public bool CanExecute(object? parameter) => _canExucuteAction?.Invoke(parameter!) ?? true;

        public void Execute(object? parameter) => _executeAction(parameter!);
    }
}

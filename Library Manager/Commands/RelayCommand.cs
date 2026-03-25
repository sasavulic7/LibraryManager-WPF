using System;
using System.Windows.Input;

namespace Library_Manager.Commands
{
    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Func<object, bool> _canExecute;

        private event EventHandler _canExecuteChanged;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; _canExecuteChanged += value; }
            remove { CommandManager.RequerySuggested -= value; _canExecuteChanged -= value; }
        }

        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public RelayCommand(Action execute, Func<bool> canExecute = null)
        {
            _execute = _ => execute();
            _canExecute = _ => canExecute == null || canExecute();
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }

        public void RaiseCanExecuteChanged()
        {
            _canExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
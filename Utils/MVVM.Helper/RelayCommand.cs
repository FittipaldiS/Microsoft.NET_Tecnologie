using System.Windows.Input;

namespace MVVM.Helper
{
    public class RelayCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private Action<object> _executeMethod;
        private Predicate<object> _canExecuteMethod;

        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            _executeMethod = execute;
            _canExecuteMethod = canExecute;
        }

        public RelayCommand(Action<object> execute) : this(execute, null)
        {

        }

        public bool CanExecute(object parameter)
        {
            return (_canExecuteMethod == null) ? true : _canExecuteMethod.Invoke(parameter);
        }

        public void Execute(object parameter)
        {
            _executeMethod.Invoke(parameter);
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}

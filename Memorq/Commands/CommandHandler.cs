using System;
using System.Windows.Input;

namespace Task.Commands
{
    /// <summary>
    /// A class that handles commands.
    /// </summary>
    public class CommandHandler : ICommand
    {
        private readonly Predicate<object> _canExecute;
        private readonly Action<object> _execute;


        public bool CanExecute(object parameter)
        {
            return _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }

        public CommandHandler(Predicate<object> canExecute, Action<object> execute)
        {
            _canExecute = canExecute;
            _execute = execute;
        }

        public event EventHandler CanExecuteChanged = (sender, e) => { };
    }
}

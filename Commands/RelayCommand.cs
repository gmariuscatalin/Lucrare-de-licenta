using System;
using System.Windows.Input;

namespace NewBank2.Commands
{
     /*RelayCommand is a simple ICommand implementation that delegates execution
       to the provided Action and allows the caller to determine if the command
       can be executed by providing an optional Predicate.*/
    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Predicate<object> _canExecute;

        /* Constructor that takes only an execute action and uses it
           for the command's execution logic.*/
        public RelayCommand(Action<object> execute)
            : this(execute, null)
        {
        }

        /* Constructor that takes both an execute action and a canExecute predicate.
           The execute action is the command's execution logic, and the canExecute
           predicate determines whether the command can be executed.*/
        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            // Ensure the execute action is not null
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        // Event that is raised when the command's execution status changes.
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        // Determines if the command can be executed by evaluating the canExecute predicate.
        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }

        // Executes the command by invoking the execute action.
        public void Execute(object parameter)
        {
            _execute(parameter);
        }
    }
}
using System;
using System.Windows.Input;

namespace FileTagger.Commands
{
    public abstract class CommandBase : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public abstract bool CanExecute(object parameter);

        public abstract void Execute(object parameter);

        public void RaiseCanExecuteChanged()
        {
            var handler = CanExecuteChanged;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }

            // TODO PRJ: Needed? Is the event enough?
            CommandManager.InvalidateRequerySuggested();
        }
    }
}

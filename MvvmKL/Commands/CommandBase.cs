using System;

namespace MvvmKL.Commands
{
    public abstract class CommandBase : ICommand
    {
        public abstract void Execute(object parameter);
        public abstract bool CanExecute(object parameter);
        public event EventHandler CanExecuteChanged;

        public void RaiseCanExecuteChanged()
        {
            EventHandler handler;
            lock (this)
            {
                handler = CanExecuteChanged;
            }

            if (handler != null)
            {
                handler.Invoke(this, EventArgs.Empty);
            }
        }

    }
}

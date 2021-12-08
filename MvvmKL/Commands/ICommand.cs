using System.Diagnostics.CodeAnalysis;

namespace MvvmKL.Commands
{
    public interface ICommand
    {
        void Execute(object parameter);
        bool CanExecute(object parameter);
        event System.EventHandler CanExecuteChanged;

        //[SuppressMessage("Microsoft.Design", "CA1030:UseEventsWhereAppropriate", Justification = "This cannot be an event")]
        void RaiseCanExecuteChanged();
    }
}

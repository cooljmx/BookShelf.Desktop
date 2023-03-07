using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BookShelf.ViewModels.Commands;

public class AsyncCommand : ICommand
{
    private readonly Func<Task> _execute;

    public AsyncCommand(Func<Task> execute)
    {
        _execute = execute;
    }

    public bool CanExecute(object parameter)
    {
        return true;
    }

    public async void Execute(object parameter)
    {
        await _execute.Invoke();
    }

    public event EventHandler CanExecuteChanged;
}
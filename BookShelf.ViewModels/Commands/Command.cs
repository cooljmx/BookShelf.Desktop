using System;
using System.Windows.Input;

namespace BookShelf.ViewModels.Commands;

public class Command : ICommand
{
    private readonly Func<bool> _canExecute;
    private readonly Action _execute;

    public Command(Action execute, Func<bool> canExecute = null)
    {
        _execute = execute;
        _canExecute = canExecute;
    }

    public bool CanExecute(object? parameter)
    {
        return _canExecute?.Invoke() ?? true;
    }

    public void Execute(object? parameter)
    {
        _execute.Invoke();
    }

    public event EventHandler? CanExecuteChanged;

    public void InvokeCanExecuteChanged()
    {
        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}
using System;
using System.Windows.Input;

namespace BookShelf.ViewModels.MainWindow;

public interface IMainWindowMenuViewModel : IDisposable
{
    ICommand CloseMainWindowCommand { get; }
    ICommand OpenAboutWindowCommand { get; }
    ICommand OpenAuthorCollectionCommand { get; }

    IDevToolsMenuViewModel DevToolsMenuViewModel { get; }

    void CloseAboutWindow();

    event Action MainWindowClosingRequested;
    event Action<IMainWindowContentViewModel> ContentViewModelChanged;
}
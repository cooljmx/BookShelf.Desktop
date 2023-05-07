using System;
using System.Windows.Input;

namespace BookShelf.ViewModels.MainWindow;

public interface IMainWindowMenuViewModel
{
    ICommand CloseMainWindowCommand { get; }
    ICommand OpenAboutWindowCommand { get; }
    ICommand OpenAuthorCollectionCommand { get; }

    void CloseAboutWindow();

    event Action MainWindowClosingRequested;
    event Action<IMainWindowContentViewModel> ContentViewModelChanged;
}
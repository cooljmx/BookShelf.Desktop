using System;
using BookShelf.Domain.Factories;
using BookShelf.Domain.Settings;
using BookShelf.ViewModels.Windows;

namespace BookShelf.ViewModels.MainWindow;

public class MainWindowViewModel : WindowViewModel<IMainWindowMementoWrapper>, IMainWindowViewModel
{
    private readonly IWindowManager _windowManager;
    private IMainWindowContentViewModel? _contentViewModel;

    public MainWindowViewModel(
        IMainWindowMementoWrapper mainWindowMementoWrapper,
        IWindowManager windowManager,
        IFactory<IMainWindowMenuViewModel> mainWindowMenuViewModelFactory,
        IFactory<IMainWindowStatusBarViewModel> mainWindowStatusBarViewModelFactory)
        : base(mainWindowMementoWrapper)
    {
        _windowManager = windowManager;

        MenuViewModel = mainWindowMenuViewModelFactory.Create();
        StatusBarViewModel = mainWindowStatusBarViewModelFactory.Create();

        MenuViewModel.ContentViewModelChanged += OnContentViewModelChanged;
        MenuViewModel.MainWindowClosingRequested += OnMainWindowClosingRequested;
    }

    public IMainWindowStatusBarViewModel StatusBarViewModel { get; }
    public IMainWindowMenuViewModel MenuViewModel { get; }
    public string Title => "Book Shelf";

    public IMainWindowContentViewModel? ContentViewModel
    {
        get => _contentViewModel;
        private set
        {
            if (_contentViewModel is IDisposable disposableViewModel)
                disposableViewModel.Dispose();

            _contentViewModel = value;

            InvokePropertyChanged();
        }
    }

    public override void WindowClosing()
    {
        base.WindowClosing();

        MenuViewModel.CloseAboutWindow();
    }

    private void OnMainWindowClosingRequested()
    {
        _windowManager.Close(this);
    }

    private void OnContentViewModelChanged(IMainWindowContentViewModel contentViewModel)
    {
        ContentViewModel = contentViewModel;
    }

    public void Dispose()
    {
        MenuViewModel.MainWindowClosingRequested -= OnMainWindowClosingRequested;
        MenuViewModel.ContentViewModelChanged -= OnContentViewModelChanged;

        ContentViewModel = null;

        StatusBarViewModel.Dispose();
        MenuViewModel.Dispose();
    }
}
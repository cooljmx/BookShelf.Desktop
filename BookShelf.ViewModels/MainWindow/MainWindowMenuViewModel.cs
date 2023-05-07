using System;
using System.Threading.Tasks;
using System.Windows.Input;
using BookShelf.Domain.Factories;
using BookShelf.ViewModels.Authors;
using BookShelf.ViewModels.Commands;
using BookShelf.ViewModels.Windows;

namespace BookShelf.ViewModels.MainWindow;

public class MainWindowMenuViewModel : IMainWindowMenuViewModel
{
    private readonly IFactory<IAboutWindowViewModel> _aboutWindowViewModelFactory;
    private readonly IFactory<IAuthorCollectionViewModel> _authorCollectionViewModelFactory;
    private readonly Command _closeMainWindowCommand;
    private readonly Command _openAboutWindowCommand;
    private readonly AsyncCommand _openAuthorCollectionCommand;
    private readonly IWindowManager _windowManager;
    private IAboutWindowViewModel? _aboutWindowViewModel;

    public MainWindowMenuViewModel(
        IWindowManager windowManager,
        IFactory<IAboutWindowViewModel> aboutWindowViewModelFactory,
        IFactory<IAuthorCollectionViewModel> authorCollectionViewModelFactory)
    {
        _windowManager = windowManager;
        _aboutWindowViewModelFactory = aboutWindowViewModelFactory;
        _authorCollectionViewModelFactory = authorCollectionViewModelFactory;

        _closeMainWindowCommand = new Command(CloseMainWindow);
        _openAboutWindowCommand = new Command(OpenAboutWindow);
        _openAuthorCollectionCommand = new AsyncCommand(OpenAuthorCollectionAsync);
    }

    public ICommand CloseMainWindowCommand => _closeMainWindowCommand;
    public ICommand OpenAboutWindowCommand => _openAboutWindowCommand;
    public ICommand OpenAuthorCollectionCommand => _openAuthorCollectionCommand;

    public void CloseAboutWindow()
    {
        if (_aboutWindowViewModel != null)
            _windowManager.Close(_aboutWindowViewModel);
    }

    public event Action? MainWindowClosingRequested;
    public event Action<IMainWindowContentViewModel>? ContentViewModelChanged;

    private void OpenAboutWindow()
    {
        if (_aboutWindowViewModel == null)
        {
            _aboutWindowViewModel = _aboutWindowViewModelFactory.Create();

            var aboutWindow = _windowManager.Show(_aboutWindowViewModel);

            aboutWindow.Closed += OnAboutWindowClosed;
        }
        else
        {
            _windowManager.Show(_aboutWindowViewModel);
        }
    }

    private void OnAboutWindowClosed(object? sender, EventArgs e)
    {
        if (sender is IWindow window)
        {
            window.Closed -= OnAboutWindowClosed;

            _aboutWindowViewModel = null;
        }
    }

    private void CloseMainWindow()
    {
        MainWindowClosingRequested?.Invoke();
    }


    private async Task OpenAuthorCollectionAsync()
    {
        var authorCollectionViewModel = _authorCollectionViewModelFactory.Create();

        await authorCollectionViewModel.InitializeAsync();

        ContentViewModelChanged?.Invoke(authorCollectionViewModel);
    }

    public void Dispose()
    {
    }
}
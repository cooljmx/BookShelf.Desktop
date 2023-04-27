using System;
using System.Threading.Tasks;
using System.Windows.Input;
using BookShelf.Domain.DispatcherTimer;
using BookShelf.Domain.Factories;
using BookShelf.Domain.Settings;
using BookShelf.Domain.Version;
using BookShelf.ViewModels.Authors;
using BookShelf.ViewModels.Commands;
using BookShelf.ViewModels.Windows;

namespace BookShelf.ViewModels.MainWindow;

public class MainWindowViewModel : WindowViewModel<IMainWindowMementoWrapper>, IMainWindowViewModel
{
    private readonly IFactory<IAboutWindowViewModel> _aboutWindowViewModelFactory;
    private readonly IFactory<IAuthorCollectionViewModel> _authorCollectionViewModelFactory;
    private readonly Command _closeMainWindowCommand;
    private readonly IDispatcherTimer _dispatcherTimer;
    private readonly Command _openAboutWindowCommand;
    private readonly AsyncCommand _openAuthorCollectionCommand;
    private readonly IWindowManager _windowManager;
    private IAboutWindowViewModel? _aboutWindowViewModel;
    private IMainWindowContentViewModel? _contentViewModel;
    private string _currentDate = string.Empty;
    private string _currentTime = string.Empty;

    public MainWindowViewModel(
        IMainWindowMementoWrapper mainWindowMementoWrapper,
        IWindowManager windowManager,
        IApplicationVersionProvider applicationVersionProvider,
        IDispatcherTimerFactory dispatcherTimerFactory,
        IFactory<IAboutWindowViewModel> aboutWindowViewModelFactory,
        IFactory<IAuthorCollectionViewModel> authorCollectionViewModelFactory)
        : base(mainWindowMementoWrapper)
    {
        _windowManager = windowManager;
        _aboutWindowViewModelFactory = aboutWindowViewModelFactory;
        _authorCollectionViewModelFactory = authorCollectionViewModelFactory;

        _closeMainWindowCommand = new Command(CloseMainWindow);
        _openAboutWindowCommand = new Command(OpenAboutWindow);
        _openAuthorCollectionCommand = new AsyncCommand(OpenAuthorCollectionAsync);
        _dispatcherTimer = dispatcherTimerFactory.Create(TimeSpan.FromSeconds(1));
        _dispatcherTimer.Tick += OnTimerTick;
        _dispatcherTimer.Start();

        Version = $"Version {applicationVersionProvider.Version.ToString(3)}";
    }

    public string Version { get; }
    public string Title => "Book Shelf";

    public string CurrentDate
    {
        get => _currentDate;
        private set
        {
            _currentDate = value;
            InvokePropertyChanged();
        }
    }

    public string CurrentTime
    {
        get => _currentTime;
        private set
        {
            _currentTime = value;
            InvokePropertyChanged();
        }
    }

    public IMainWindowContentViewModel? ContentViewModel
    {
        get => _contentViewModel;
        private set
        {
            _contentViewModel = value;

            InvokePropertyChanged();
        }
    }

    public ICommand CloseMainWindowCommand => _closeMainWindowCommand;
    public ICommand OpenAboutWindowCommand => _openAboutWindowCommand;
    public ICommand OpenAuthorCollectionCommand => _openAuthorCollectionCommand;

    public override void WindowClosing()
    {
        base.WindowClosing();

        if (_aboutWindowViewModel != null)
            _windowManager.Close(_aboutWindowViewModel);
    }

    private async Task OpenAuthorCollectionAsync()
    {
        var authorCollectionViewModel = _authorCollectionViewModelFactory.Create();

        await authorCollectionViewModel.InitializeAsync();

        ContentViewModel = authorCollectionViewModel;
    }

    private void OnTimerTick(object? sender, EventArgs e)
    {
        CurrentDate = DateTime.Now.ToShortDateString();
        CurrentTime = DateTime.Now.ToLongTimeString();
    }

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
        _windowManager.Close(this);
    }

    public void Dispose()
    {
        _dispatcherTimer.Tick -= OnTimerTick;
    }
}
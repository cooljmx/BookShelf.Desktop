using System;
using System.Windows.Input;
using BookShelf.Domain.DispatcherTimer;
using BookShelf.Domain.Factories;
using BookShelf.Domain.Settings;
using BookShelf.Domain.Version;
using BookShelf.ViewModels.Commands;
using BookShelf.ViewModels.Genres;
using BookShelf.ViewModels.Windows;

namespace BookShelf.ViewModels.MainWindow;

public class MainWindowViewModel : WindowViewModel<IMainWindowMementoWrapper>, IMainWindowViewModel
{
    private readonly IFactory<IAboutWindowViewModel> _aboutWindowViewModelFactory;
    private readonly Command _closeMainWindowCommand;
    private readonly IDispatcherTimer _dispatcherTimer;
    private readonly IFactory<IGenreCollectionViewModel> _genreCollectionViewModelFactory;
    private readonly Command _openAboutWindowCommand;
    private readonly Command _openGenreCollectionCommand;
    private readonly IWindowManager _windowManager;
    private IMainWindowContentViewModel _contentViewModel;
    private string _currentDate;
    private string _currentTime;

    public MainWindowViewModel(
        IMainWindowMementoWrapper mainWindowMementoWrapper,
        IWindowManager windowManager,
        IFactory<IGenreCollectionViewModel> genreCollectionViewModelFactory,
        IFactory<IAboutWindowViewModel> aboutWindowViewModelFactory,
        IApplicationVersionProvider applicationVersionProvider,
        IDispatcherTimerFactory dispatcherTimerFactory)
        : base(mainWindowMementoWrapper)
    {
        _windowManager = windowManager;
        _genreCollectionViewModelFactory = genreCollectionViewModelFactory;
        _aboutWindowViewModelFactory = aboutWindowViewModelFactory;

        _closeMainWindowCommand = new Command(CloseMainWindow);
        _openGenreCollectionCommand = new Command(OpenGenreCollection);
        _openAboutWindowCommand = new Command(OpenAboutWindow);
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
            InvokePropertyChanged(nameof(CurrentDate));
        }
    }

    public string CurrentTime
    {
        get => _currentTime;
        private set
        {
            _currentTime = value;
            InvokePropertyChanged(nameof(CurrentTime));
        }
    }

    public ICommand CloseMainWindowCommand => _closeMainWindowCommand;
    public ICommand OpenGenreCollectionCommand => _openGenreCollectionCommand;
    public ICommand OpenAboutWindowCommand => _openAboutWindowCommand;

    public IMainWindowContentViewModel ContentViewModel
    {
        get => _contentViewModel;
        private set
        {
            _contentViewModel = value;

            InvokePropertyChanged(nameof(ContentViewModel));
        }
    }

    public override void AfterWindowClosed()
    {
        ContentViewModel?.Dispose();

        base.AfterWindowClosed();
    }

    private void OnTimerTick(object sender, EventArgs e)
    {
        CurrentDate = DateTime.Now.ToShortDateString();
        CurrentTime = DateTime.Now.ToLongTimeString();
    }

    private void OpenAboutWindow()
    {
        var aboutWindowViewModel = _aboutWindowViewModelFactory.Create();

        _windowManager.Show(aboutWindowViewModel, true);
    }

    private void OpenGenreCollection()
    {
        ContentViewModel?.Dispose();
        ContentViewModel = _genreCollectionViewModelFactory.Create();
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
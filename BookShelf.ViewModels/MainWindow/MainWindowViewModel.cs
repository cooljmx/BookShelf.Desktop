using System;
using System.Windows.Input;
using BookShelf.Domain.DispatcherTimer;
using BookShelf.Domain.Settings;
using BookShelf.Domain.Version;
using BookShelf.ViewModels.Commands;
using BookShelf.ViewModels.Windows;

namespace BookShelf.ViewModels.MainWindow;

public class MainWindowViewModel : WindowViewModel<IMainWindowMementoWrapper>, IMainWindowViewModel
{
    private readonly IAboutWindowViewModel _aboutWindowViewModel;
    private readonly Command _closeMainWindowCommand;
    private readonly IDispatcherTimer _dispatcherTimer;
    private readonly Command _openAboutWindowCommand;
    private readonly IWindowManager _windowManager;
    private string _currentDate;
    private string _currentTime;

    public MainWindowViewModel(
        IMainWindowMementoWrapper mainWindowMementoWrapper,
        IWindowManager windowManager,
        IAboutWindowViewModel aboutWindowViewModel,
        IApplicationVersionProvider applicationVersionProvider,
        IDispatcherTimerFactory dispatcherTimerFactory)
        : base(mainWindowMementoWrapper)
    {
        _windowManager = windowManager;
        _aboutWindowViewModel = aboutWindowViewModel;

        _closeMainWindowCommand = new Command(CloseMainWindow);
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
    public ICommand OpenAboutWindowCommand => _openAboutWindowCommand;

    public override void WindowClosing()
    {
        base.WindowClosing();

        _windowManager.Close(_aboutWindowViewModel);
    }

    private void OnTimerTick(object sender, EventArgs e)
    {
        CurrentDate = DateTime.Now.ToShortDateString();
        CurrentTime = DateTime.Now.ToLongTimeString();
    }

    private void OpenAboutWindow()
    {
        _windowManager.Show(_aboutWindowViewModel);
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
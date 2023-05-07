using System;
using BookShelf.Domain.DispatcherTimer;
using BookShelf.Domain.Version;

namespace BookShelf.ViewModels.MainWindow;

public class MainWindowStatusBarViewModel : ViewModel, IMainWindowStatusBarViewModel
{
    private readonly IDispatcherTimer _dispatcherTimer;

    public MainWindowStatusBarViewModel(
        IApplicationVersionProvider applicationVersionProvider,
        IDispatcherTimerFactory dispatcherTimerFactory)
    {
        _dispatcherTimer = dispatcherTimerFactory.Create(TimeSpan.FromSeconds(1));
        _dispatcherTimer.Tick += OnTimerTick;
        _dispatcherTimer.Start();

        Version = $"Version {applicationVersionProvider.Version.ToString(3)}";
    }

    public string Version { get; }
    public string CurrentDate => DateTime.Now.ToShortDateString();
    public string CurrentTime => DateTime.Now.ToLongTimeString();


    private void OnTimerTick(object? sender, EventArgs e)
    {
        InvokePropertyChanged(nameof(CurrentDate));
        InvokePropertyChanged(nameof(CurrentTime));
    }

    public void Dispose()
    {
        _dispatcherTimer.Tick -= OnTimerTick;
    }
}
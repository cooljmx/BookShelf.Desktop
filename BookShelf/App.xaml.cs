using System.Windows;
using System.Windows.Threading;
using BookShelf.Bootstrapper;

namespace BookShelf;

public partial class App
{
    private ApplicationBootstrapper? _bootstrapper;

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        _bootstrapper = new ApplicationBootstrapper();

        DispatcherUnhandledException += OnUnhandledExceptionRaised;

        var application = _bootstrapper.CreateApplication();

        MainWindow = application.Run();
    }

    protected override void OnExit(ExitEventArgs e)
    {
        DispatcherUnhandledException -= OnUnhandledExceptionRaised;
        _bootstrapper?.Dispose();

        base.OnExit(e);
    }

    private void OnUnhandledExceptionRaised(object sender, DispatcherUnhandledExceptionEventArgs e)
    {
        if (_bootstrapper is null)
            return;

        var unhandledExceptionHandler = _bootstrapper.CreateUnhandledExceptionHandler();

        unhandledExceptionHandler.Handle(e);
    }
}
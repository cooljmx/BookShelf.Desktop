using System.Windows;
using BookShelf.Bootstrapper;

namespace BookShelf;

public partial class App
{
    private ApplicationBootstrapper? _bootstrapper;

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        _bootstrapper = new ApplicationBootstrapper();

        var application = _bootstrapper.CreateApplication();

        MainWindow = application.Run();
    }

    protected override void OnExit(ExitEventArgs e)
    {
        _bootstrapper?.Dispose();

        base.OnExit(e);
    }
}
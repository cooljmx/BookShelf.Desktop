using System.Windows;

namespace BookShelf;

public partial class App
{
    private Bootstrapper.Bootstrapper _bootstrapper;

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        _bootstrapper = new Bootstrapper.Bootstrapper();

        MainWindow = _bootstrapper.Run();
    }

    protected override void OnExit(ExitEventArgs e)
    {
        _bootstrapper.Dispose();

        base.OnExit(e);
    }
}
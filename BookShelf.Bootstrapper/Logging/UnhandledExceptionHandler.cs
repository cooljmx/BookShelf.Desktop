using System.Windows.Threading;
using NLog;

namespace BookShelf.Bootstrapper.Logging;

internal class UnhandledExceptionHandler : IUnhandledExceptionHandler
{
    private static readonly ILogger Logger = LogManager.GetLogger(nameof(UnhandledExceptionHandler));

    public void Handle(DispatcherUnhandledExceptionEventArgs args)
    {
        Logger.Error(args.Exception);

        args.Handled = true;
    }
}
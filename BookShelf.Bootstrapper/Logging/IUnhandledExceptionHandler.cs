using System.Windows.Threading;

namespace BookShelf.Bootstrapper.Logging;

public interface IUnhandledExceptionHandler
{
    void Handle(DispatcherUnhandledExceptionEventArgs args);
}
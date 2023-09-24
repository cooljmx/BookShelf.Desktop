using BookShelf.Domain.Logging;

namespace BookShelf.Bootstrapper.Logging;

public interface ILogNotifier
{
    void Notify(LogArgs args);
}
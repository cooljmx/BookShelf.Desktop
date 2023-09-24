using System;
using BookShelf.Domain.Logging;

namespace BookShelf.Bootstrapper.Logging;

internal class LogNotifier : ILogNotifier, ILogSubscriber
{
    public void Notify(LogArgs args)
    {
        LogAdded?.Invoke(args);
    }

    public event Action<LogArgs>? LogAdded;
}
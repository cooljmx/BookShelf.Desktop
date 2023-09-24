using System;

namespace BookShelf.Domain.Logging;

public interface ILogSubscriber
{
    event Action<LogArgs> LogAdded;
}
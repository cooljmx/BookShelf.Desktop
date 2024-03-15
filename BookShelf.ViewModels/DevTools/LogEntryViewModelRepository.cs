using System;
using BookShelf.Domain.Collections;
using BookShelf.Domain.Logging;

namespace BookShelf.ViewModels.DevTools;

internal class LogEntryViewModelRepository : ILogEntryViewModelRepository, IDisposable
{
    private readonly IRotatableCollection<LogEntryViewModel> _items;
    private readonly ILogSubscriber _logSubscriber;

    public LogEntryViewModelRepository(
        IRotatableCollectionFactory rotatableCollectionFactory,
        ILogSubscriber logSubscriber)
    {
        _logSubscriber = logSubscriber;

        _items = rotatableCollectionFactory.Create<LogEntryViewModel>(1000);

        _logSubscriber.LogAdded += OnLogAdded;
    }

    public IRotatableReadOnlyCollection<LogEntryViewModel> Items => _items;

    public void Clear()
    {
        _items.Clear();
    }

    private void OnLogAdded(LogArgs args)
    {
        var logEntryViewModel = new LogEntryViewModel
        {
            Timestamp = args.Timestamp,
            LoggerName = args.LoggerName,
            Message = args.Message,
            Level = args.LogLevel,
            TimestampValue = args.Timestamp.ToString("F"),
            StackTrace = args.StackTrace
        };

        _items.Add(logEntryViewModel);
    }

    public void Dispose()
    {
        _logSubscriber.LogAdded -= OnLogAdded;
    }
}
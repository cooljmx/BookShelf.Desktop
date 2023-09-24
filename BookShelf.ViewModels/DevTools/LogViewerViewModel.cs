using System.Collections.Generic;
using System.Collections.ObjectModel;
using BookShelf.Domain.Logging;

namespace BookShelf.ViewModels.DevTools;

public class LogViewerViewModel : ILogViewerViewModel
{
    private readonly ObservableCollection<LogEntryViewModel> _logEntryViewModels;
    private readonly ILogSubscriber _logSubscriber;

    public LogViewerViewModel(ILogSubscriber logSubscriber)
    {
        _logSubscriber = logSubscriber;

        _logEntryViewModels = new ObservableCollection<LogEntryViewModel>();

        _logSubscriber.LogAdded += OnLogAdded;
    }

    public IEnumerable<LogEntryViewModel> LogEntryViewModels => _logEntryViewModels;

    private void OnLogAdded(LogArgs args)
    {
        var logEntryViewModel = new LogEntryViewModel
        {
            LoggerName = args.LoggerName,
            Message = args.Message,
            Level = args.LogLevel,
            Timestamp = args.Timestamp.ToLongDateString(),
            StackTrace = args.StackTrace
        };

        _logEntryViewModels.Add(logEntryViewModel);
    }

    public void Dispose()
    {
        _logSubscriber.LogAdded -= OnLogAdded;
    }
}
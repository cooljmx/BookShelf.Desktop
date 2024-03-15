using System.Collections.Generic;

namespace BookShelf.ViewModels.DevTools;

public class LogViewerViewModel : ILogViewerViewModel
{
    private readonly ILogEntryViewModelRepository _logEntryViewModelRepository;

    public LogViewerViewModel(ILogEntryViewModelRepository logEntryViewModelRepository)
    {
        _logEntryViewModelRepository = logEntryViewModelRepository;
    }

    public IEnumerable<LogEntryViewModel> LogEntryViewModels => _logEntryViewModelRepository.Items;
}
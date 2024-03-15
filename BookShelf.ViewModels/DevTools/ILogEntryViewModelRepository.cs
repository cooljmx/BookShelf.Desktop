using BookShelf.Domain.Collections;

namespace BookShelf.ViewModels.DevTools;

public interface ILogEntryViewModelRepository
{
    IRotatableReadOnlyCollection<LogEntryViewModel> Items { get; }
    void Clear();
}
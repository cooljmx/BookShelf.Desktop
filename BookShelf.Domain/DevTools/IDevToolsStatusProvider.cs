namespace BookShelf.Domain.DevTools;

public interface IDevToolsStatusProvider
{
    bool IsEnabled { get; }
}
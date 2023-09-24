using BookShelf.Domain.DevTools;

namespace BookShelf.Infrastructure.DevTools;

internal class DevToolsStatusProvider : IDevToolsStatusProvider
{
    public bool IsEnabled =>
#if DEBUG
        true;
#else
        false;
#endif
}
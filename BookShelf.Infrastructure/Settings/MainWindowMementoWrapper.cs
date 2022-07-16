using BookShelf.Domain.Settings;
using BookShelf.Infrastructure.Common;

namespace BookShelf.Infrastructure.Settings;

internal class MainWindowMementoWrapper : WindowMementoWrapper<MainWindowMemento>, IMainWindowMementoWrapper
{
    public MainWindowMementoWrapper(IPathService pathService)
        : base(pathService)
    {
    }

    protected override string MementoName => "MainWindowMemento";
}
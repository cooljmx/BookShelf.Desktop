using BookShelf.Domain.Settings;
using BookShelf.Infrastructure.Common;

namespace BookShelf.Infrastructure.Settings;

internal class GenreWindowMementoWrapper : WindowMementoWrapper<GenreWindowMemento>, IGenreWindowMementoWrapper
{
    public GenreWindowMementoWrapper(IPathService pathService)
        : base(pathService)
    {
    }

    protected override string MementoName => "GenreWindowMemento";
}
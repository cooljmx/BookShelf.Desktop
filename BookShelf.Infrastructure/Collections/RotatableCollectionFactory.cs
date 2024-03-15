using BookShelf.Domain.Collections;

namespace BookShelf.Infrastructure.Collections;

internal class RotatableCollectionFactory : IRotatableCollectionFactory
{
    public IRotatableCollection<TItem> Create<TItem>(int capacity)
    {
        return new RotatableCollection<TItem>(capacity);
    }
}
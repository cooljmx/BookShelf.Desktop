namespace BookShelf.Domain.Collections;

public interface IRotatableCollection<TItem> : IRotatableReadOnlyCollection<TItem>
{
    void Add(TItem item);
    void Clear();
}
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using BookShelf.Domain.Collections;

namespace BookShelf.Infrastructure.Collections;

internal class RotatableCollection<TItem> : IRotatableCollection<TItem>
{
    private readonly int _capacity;
    private readonly ObservableCollection<TItem> _items;

    public RotatableCollection(int capacity)
    {
        _capacity = capacity;

        _items = new ObservableCollection<TItem>();
    }

    public IEnumerator<TItem> GetEnumerator()
    {
        return _items.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public event NotifyCollectionChangedEventHandler? CollectionChanged
    {
        add => _items.CollectionChanged += value;
        remove => _items.CollectionChanged -= value;
    }

    public void Add(TItem item)
    {
        if (_items.Count == _capacity)
            _items.RemoveAt(0);

        _items.Add(item);
    }

    public void Clear()
    {
        _items.Clear();
    }
}
using System.Collections.Generic;
using System.Collections.Specialized;

namespace BookShelf.Domain.Collections;

public interface IRotatableReadOnlyCollection<out TItem> : IEnumerable<TItem>, INotifyCollectionChanged
{
}
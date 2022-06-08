using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace BookShelf.Domain.Genres;

public interface IGenreRepository : INotifyCollectionChanged
{
    IReadOnlyCollection<IGenre> Items { get; }

    void Add(IGenre genre);
    void Remove(Guid id);
    IGenre Get(Guid id);
}
using System;

namespace BookShelf.ViewModels.Genres;

public interface IGenreCollectionItemViewModel : IDisposable
{
    Guid Id { get; }
}
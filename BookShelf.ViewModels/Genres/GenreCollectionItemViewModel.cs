using System;
using BookShelf.Domain.Genres;

namespace BookShelf.ViewModels.Genres;

public class GenreCollectionItemViewModel : ViewModel, IGenreCollectionItemViewModel
{
    private readonly IGenre _genre;

    public GenreCollectionItemViewModel(IGenre genre)
    {
        _genre = genre;

        _genre.Updated += OnGenreUpdated;
    }

    public string Name => $"{_genre.Name} ({_genre.Description})";

    public Guid Id => _genre.Id;

    private void OnGenreUpdated()
    {
        InvokeAllPropertiesChanged();
    }

    public void Dispose()
    {
        _genre.Updated -= OnGenreUpdated;
    }
}
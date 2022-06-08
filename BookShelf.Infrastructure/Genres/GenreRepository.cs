using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using BookShelf.Domain.Genres;
using BookShelf.Infrastructure.Storage;

namespace BookShelf.Infrastructure.Genres;

internal class GenreRepository : IGenreRepository, IDisposable
{
    private readonly IGenreDtoStorage _genreDtoStorage;
    private readonly Dictionary<Guid, IGenre> _genres = new();
    private readonly IGenreTranslator _genreTranslator;

    public GenreRepository(
        IGenreDtoStorage genreDtoStorage,
        IGenreTranslator genreTranslator)
    {
        _genreDtoStorage = genreDtoStorage;
        _genreTranslator = genreTranslator;

        LoadValues();
    }

    public event NotifyCollectionChangedEventHandler? CollectionChanged;

    public IReadOnlyCollection<IGenre> Items => _genres.Values;

    public void Add(IGenre genre)
    {
        if (_genres.TryAdd(genre.Id, genre))
        {
            CollectionChanged?.Invoke(
                this,
                new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, genre));

            SaveValues();
        }
    }

    public void Remove(Guid id)
    {
        if (_genres.Remove(id, out var genre))
        {
            CollectionChanged?.Invoke(
                this,
                new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, genre));

            SaveValues();
        }
    }

    public IGenre Get(Guid id)
    {
        if (_genres.TryGetValue(id, out var genre))
            return genre;

        throw new KeyNotFoundException();
    }

    private void LoadValues()
    {
        var dtos = _genreDtoStorage.Load();

        foreach (var dto in dtos)
        {
            var genre = _genreTranslator.ToDomain(dto);

            _genres.Add(genre.Id, genre);
        }
    }

    private void SaveValues()
    {
        var dtos = _genres.Values
            .Select(_genreTranslator.ToDto)
            .ToArray();

        _genreDtoStorage.Save(dtos);
    }

    public void Dispose()
    {
        SaveValues();
    }
}
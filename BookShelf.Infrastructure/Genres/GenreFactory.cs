using System;
using BookShelf.Domain.Genres;

namespace BookShelf.Infrastructure.Genres;

internal class GenreFactory : IGenreFactory
{
    public IGenre Create()
    {
        return new Genre(Guid.NewGuid(), null, null);
    }
}
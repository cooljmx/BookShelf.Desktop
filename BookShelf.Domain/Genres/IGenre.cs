using System;

namespace BookShelf.Domain.Genres;

public interface IGenre : IGenreData
{
    event Action Updated;

    void Update(IGenreData genreData);
}
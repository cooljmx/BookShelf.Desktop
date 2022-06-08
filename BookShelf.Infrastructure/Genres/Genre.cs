using System;
using BookShelf.Domain.Genres;

namespace BookShelf.Infrastructure.Genres;

internal class Genre : IGenre
{
    public Genre(
        Guid id,
        string name,
        string description)
    {
        Id = id;
        Name = name;
        Description = description;
    }

    public Guid Id { get; }
    public string Name { get; private set; }
    public string Description { get; private set; }

    public event Action Updated;

    public void Update(IGenreData genreData)
    {
        Name = genreData.Name;
        Description = genreData.Description;

        Updated?.Invoke();
    }
}
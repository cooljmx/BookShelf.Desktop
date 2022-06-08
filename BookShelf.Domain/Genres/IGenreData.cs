using System;

namespace BookShelf.Domain.Genres;

public interface IGenreData
{
    Guid Id { get; }
    string Name { get; }
    string Description { get; }
}
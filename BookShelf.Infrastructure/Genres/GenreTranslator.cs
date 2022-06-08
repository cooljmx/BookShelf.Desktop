using BookShelf.Domain.Genres;
using BookShelf.Infrastructure.Storage;

namespace BookShelf.Infrastructure.Genres;

internal class GenreTranslator : IGenreTranslator
{
    public IGenre ToDomain(GenreDto dto)
    {
        return new Genre(
            dto.Id,
            dto.Name,
            dto.Description);
    }

    public GenreDto ToDto(IGenre genre)
    {
        return new GenreDto
        {
            Id = genre.Id,
            Name = genre.Name,
            Description = genre.Description
        };
    }
}
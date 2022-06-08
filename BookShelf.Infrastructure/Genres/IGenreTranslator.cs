using BookShelf.Domain.Genres;
using BookShelf.Infrastructure.Storage;

namespace BookShelf.Infrastructure.Genres;

public interface IGenreTranslator
{
    IGenre ToDomain(GenreDto dto);
    GenreDto ToDto(IGenre genre);
}
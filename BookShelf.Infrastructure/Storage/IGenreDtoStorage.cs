using System.Collections.Generic;

namespace BookShelf.Infrastructure.Storage;

public interface IGenreDtoStorage
{
    void Save(IEnumerable<GenreDto> dtos);
    IEnumerable<GenreDto> Load();
}
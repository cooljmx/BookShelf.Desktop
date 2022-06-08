using System;
using System.Collections.Generic;
using System.IO;
using BookShelf.Infrastructure.Common;
using BookShelf.Infrastructure.Serialization;

namespace BookShelf.Infrastructure.Storage;

internal class GenreDtoStorage : IGenreDtoStorage, IGenreDtoStorageInitializer
{
    private readonly IJsonService _jsonService;
    private readonly IPathService _pathService;
    private string _dataFilePath;
    private bool _initialized;

    public GenreDtoStorage(
        IJsonService jsonService,
        IPathService pathService)
    {
        _jsonService = jsonService;
        _pathService = pathService;
    }

    public void Save(IEnumerable<GenreDto> dtos)
    {
        var jsonValue = _jsonService.Serialize(dtos);

        File.WriteAllText(_dataFilePath, jsonValue);
    }

    public IEnumerable<GenreDto> Load()
    {
        var jsonValue = File.ReadAllText(_dataFilePath);

        return _jsonService.Deserialize<IEnumerable<GenreDto>>(jsonValue);
    }

    public void Initialize()
    {
        if (_initialized)
            throw new InvalidOperationException($"{nameof(IGenreDtoStorage)} is already initialized");

        _initialized = true;

        const string dataFolderName = "Data";

        var dataFolderPath = Path.Combine(_pathService.ApplicationFolder, dataFolderName);
        _dataFilePath = Path.Combine(_pathService.ApplicationFolder, dataFolderName, "genres.json");

        Directory.CreateDirectory(dataFolderPath);
    }
}
using System;
using Newtonsoft.Json;

namespace BookShelf.Infrastructure.Storage;

public class GenreDto
{
    [JsonProperty(PropertyName = "id")]
    public Guid Id { get; init; }

    [JsonProperty(PropertyName = "name")]
    public string Name { get; init; }

    [JsonProperty(PropertyName = "description")]
    public string Description { get; set; }
}
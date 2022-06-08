namespace BookShelf.Infrastructure.Serialization;

public interface IJsonService
{
    string Serialize(object value);
    T Deserialize<T>(string value);
}
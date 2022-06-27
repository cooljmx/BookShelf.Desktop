namespace BookShelf.Domain.Version;

public interface IApplicationVersionProvider
{
    System.Version Version { get; }
}
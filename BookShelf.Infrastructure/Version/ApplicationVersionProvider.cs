using BookShelf.Domain.Version;

namespace BookShelf.Infrastructure.Version;

internal class ApplicationVersionProvider : IApplicationVersionProvider
{
    public System.Version Version { get; } = new(1, 0, 0);
}
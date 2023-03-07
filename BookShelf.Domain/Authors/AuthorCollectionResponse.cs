using System.Collections.Generic;

namespace BookShelf.Domain.Authors;

public class AuthorCollectionResponse
{
    public required IEnumerable<AuthorResponse> Items { get; init; }
}
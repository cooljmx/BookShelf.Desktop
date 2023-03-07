using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookShelf.Domain.Authors;
using BookShelf.Domain.Rest;

namespace BookShelf.ViewModels.Authors;

public class AuthorCollectionViewModel : IAuthorCollectionViewModel
{
    private readonly IApiRequestExecutor _apiRequestExecutor;

    public AuthorCollectionViewModel(IApiRequestExecutor apiRequestExecutor)
    {
        _apiRequestExecutor = apiRequestExecutor;
    }

    public IEnumerable<AuthorCollectionItemViewModel> Items { get; private set; } =
        Enumerable.Empty<AuthorCollectionItemViewModel>();

    public async Task InitializeAsync()
    {
        var authorCollectionResponse = await _apiRequestExecutor.GetAsync<AuthorCollectionResponse>("author/all");

        Items = authorCollectionResponse.Items
            .Select(response => new AuthorCollectionItemViewModel(
                response.FirstName,
                response.LastName,
                response.BirthDate))
            .ToArray();
    }
}
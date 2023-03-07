using System.Threading.Tasks;
using BookShelf.ViewModels.MainWindow;

namespace BookShelf.ViewModels.Authors;

public interface IAuthorCollectionViewModel : IMainWindowContentViewModel
{
    Task InitializeAsync();
}
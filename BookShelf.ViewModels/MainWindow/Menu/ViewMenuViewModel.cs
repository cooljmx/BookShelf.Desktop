using BookShelf.Domain.Factories;

namespace BookShelf.ViewModels.MainWindow.Menu;

public sealed class ViewMenuViewModel : IViewMenuViewModel
{
    public ViewMenuViewModel(IFactory<IThemesMenuViewModel> themesMenuViewModelFactory)
    {
        ThemesMenuViewModel = themesMenuViewModelFactory.Create();
    }

    public IThemesMenuViewModel ThemesMenuViewModel { get; }
}
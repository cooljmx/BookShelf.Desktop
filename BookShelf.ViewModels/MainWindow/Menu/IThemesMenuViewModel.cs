using System.Windows.Input;

namespace BookShelf.ViewModels.MainWindow.Menu;

public interface IThemesMenuViewModel
{
    ICommand SetLightThemeCommand { get; }
    ICommand SetDarkThemeCommand { get; }
}
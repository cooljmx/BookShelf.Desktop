using System.Windows.Input;
using BookShelf.ViewModels.Commands;
using BookShelf.ViewModels.Themes;

namespace BookShelf.ViewModels.MainWindow.Menu;

public sealed class ThemesMenuViewModel : IThemesMenuViewModel
{
    private readonly IThemeManager _themeManager;

    public ThemesMenuViewModel(IThemeManager themeManager)
    {
        _themeManager = themeManager;

        SetLightThemeCommand = new Command(SetLightTheme);
        SetDarkThemeCommand = new Command(SetDarkTheme);
    }

    public ICommand SetLightThemeCommand { get; }
    public ICommand SetDarkThemeCommand { get; }

    private void SetDarkTheme()
    {
        _themeManager.SwitchTo(Theme.Dark);
    }

    private void SetLightTheme()
    {
        _themeManager.SwitchTo(Theme.Light);
    }
}
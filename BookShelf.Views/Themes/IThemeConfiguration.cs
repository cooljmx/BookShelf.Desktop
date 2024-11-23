using BookShelf.ViewModels.Themes;

namespace BookShelf.Views.Themes;

internal interface IThemeConfiguration
{
    Theme Theme { get; }
    void Apply();
}
using System.Windows.Media;
using BookShelf.ViewModels.Themes;

namespace BookShelf.Views.Themes;

internal sealed class LightThemeConfiguration : ThemeConfiguration
{
    protected override SolidColorBrush PrimaryBackground { get; } =
        CreateSolidColorBrush(Color.FromRgb(0xF3, 0xF3, 0xF3));

    public override Theme Theme => Theme.Light;
}
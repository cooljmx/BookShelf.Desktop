using System.Windows.Media;
using BookShelf.ViewModels.Themes;

namespace BookShelf.Views.Themes;

internal sealed class DarkThemeConfiguration : ThemeConfiguration
{
    protected override SolidColorBrush PrimaryBackground { get; } =
        CreateSolidColorBrush(Color.FromRgb(0x1A, 0x1A, 0x1A));

    public override Theme Theme => Theme.Dark;
}
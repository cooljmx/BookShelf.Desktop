using System.Windows.Media;
using BookShelf.ViewModels.Themes;

namespace BookShelf.Views.Themes;

internal abstract class ThemeConfiguration : IThemeConfiguration
{
    protected abstract SolidColorBrush PrimaryBackground { get; }
    public abstract Theme Theme { get; }

    public void Apply()
    {
        ThemeResources.Properties.PrimaryBackground = PrimaryBackground;
    }

    protected static SolidColorBrush CreateSolidColorBrush(Color color, double opacity = 1, bool isFrozen = true)
    {
        var solidColorBrush = new SolidColorBrush(color) { Opacity = opacity };

        if (isFrozen)
            solidColorBrush.Freeze();

        return solidColorBrush;
    }
}
using System.Collections.Frozen;
using System.Collections.Generic;
using BookShelf.ViewModels.Themes;

namespace BookShelf.Views.Themes;

internal sealed class ThemeManager : IThemeManager
{
    private readonly FrozenDictionary<Theme, IThemeConfiguration> _themeConfigurations;

    public ThemeManager(IEnumerable<IThemeConfiguration> themeConfigurations)
    {
        _themeConfigurations = themeConfigurations.ToFrozenDictionary(configuration => configuration.Theme);
    }

    public void SwitchTo(Theme theme)
    {
        if (_themeConfigurations.TryGetValue(theme, out var themeConfiguration))
            themeConfiguration.Apply();
    }
}
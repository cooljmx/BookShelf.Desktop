using System.Windows;
using System.Windows.Media;

namespace BookShelf.Views.Themes;

internal sealed class ThemeProperties : DependencyObject
{
    #region PrimaryBackground

    public static readonly DependencyProperty PrimaryBackgroundProperty = DependencyProperty.Register(
        nameof(PrimaryBackground), typeof(SolidColorBrush), typeof(ThemeProperties),
        new FrameworkPropertyMetadata(default(SolidColorBrush))
        {
            BindsTwoWayByDefault = false
        });

    public SolidColorBrush PrimaryBackground
    {
        get => (SolidColorBrush)GetValue(PrimaryBackgroundProperty);
        set => SetValue(PrimaryBackgroundProperty, value);
    }

    #endregion
}
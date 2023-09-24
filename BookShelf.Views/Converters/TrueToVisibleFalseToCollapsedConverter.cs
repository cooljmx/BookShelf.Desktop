using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace BookShelf.Views.Converters;

public class TrueToVisibleFalseToCollapsedConverter : IValueConverter
{
    public static readonly IValueConverter Instance = new TrueToVisibleFalseToCollapsedConverter();

    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value is true
            ? Visibility.Visible
            : Visibility.Collapsed;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not Visibility visibility)
            return false;

        return visibility switch
        {
            Visibility.Visible => true,
            Visibility.Hidden => false,
            Visibility.Collapsed => false,
            _ => false
        };
    }
}
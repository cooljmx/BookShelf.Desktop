using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace BookShelf.Views.Converters
{
    public class BoolToWindowStateConverter : IValueConverter
    {
        public static readonly IValueConverter Instance = new BoolToWindowStateConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isMaximized)
                return isMaximized
                    ? WindowState.Maximized
                    : WindowState.Normal;

            return WindowState.Normal;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is WindowState windowState)
                return windowState == WindowState.Maximized;

            return false;
        }
    }
}
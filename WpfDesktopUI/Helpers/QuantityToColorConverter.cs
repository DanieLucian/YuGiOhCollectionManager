using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace WpfDesktopUI.Helpers;

public class QuantityToColorConverter : IValueConverter
{

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is int val)
        {
            var brush = new SolidColorBrush { Opacity = 0.5, Color = Colors.Transparent };

            if (val > 0)
            {
                brush.Color = Colors.LawnGreen;
            }

            else if (val < 0)
            {
                brush.Color = Colors.Red;
            }

            return brush;
        }

        return "Invalid";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }

}
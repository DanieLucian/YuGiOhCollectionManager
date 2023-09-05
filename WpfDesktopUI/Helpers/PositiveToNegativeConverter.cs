using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using Xceed.Wpf.Toolkit;

namespace WpfDesktopUI.Helpers
{
    public class PositiveToNegativeConverter : IValueConverter
    {

        public object Convert(object? value, Type targetType, object parameter, CultureInfo culture)
        {

            if (value is int val)
            {
                if (val > 0)

                {
                    return -val;
                }

                else if (val == 0)
                {
                    return 0;
                }
            }

            return "Invalid data type";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }
}

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WpfDesktopUI.Helpers
{
    internal class LinkArrowConverter : IValueConverter
    {

        private static Dictionary<string, double> ArrowAnglePairs
        {
            get;
        } = new()
        {
            { "Top-Left", -45 },
            { "Top", 0 },
            { "Top-Right", 45 },
            { "Left", -90 },
            { "Right", 90 },
            { "Bottom-Left", -135 },
            { "Bottom", -180 },
            { "Bottom-Right", 135 }
        };

        public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string[] arrows)
            {
                byte counter = 0;
                double x = 11;
                double y = -x / 2;
                double centerX = x / 2;
                double centerY = y / 3;

                UniformGrid grid = new()
                                   {
                                       Rows = 3,
                                       Columns = 3,
                                       Margin = new Thickness(0, 0, 5, 0),
                                   };

                foreach (var arrow in ArrowAnglePairs)
                {
                    if (counter == 4)
                    {
                        grid.Children.Add(new Border());
                    }

                    Polygon triangle = new()
                                       {
                                           Points =
                                           new PointCollection()
                                           {
                                               new Point(0, 0),
                                               new Point(x, 0),
                                               new Point(x / 2, y)
                                           },
                                           Stroke = Brushes.Black,
                                           StrokeThickness = 0.35,
                                           LayoutTransform = new RotateTransform(arrow.Value, centerX, centerY),
                                           HorizontalAlignment = HorizontalAlignment.Center,
                                           VerticalAlignment = VerticalAlignment.Center,
                                           Margin = new Thickness(-1.25)
                                       };

                    if (arrows.Any(a => a.Equals(arrow.Key, StringComparison.OrdinalIgnoreCase)))
                    {
                        triangle.Fill = Brushes.OrangeRed;
                    }

                    else
                    {
                        triangle.Fill = Brushes.LightGray;
                    }

                    grid.Children.Add(triangle);

                    counter++;
                }

                return grid;
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }
}

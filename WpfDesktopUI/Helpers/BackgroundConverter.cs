using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace WpfDesktopUI.Helpers
{
    public class BackgroundConverter : IValueConverter
    {
        private static Dictionary<string, Color> FrameColors { get; } = new()
        {
            { "Normal", Color.FromArgb(255, 253, 230, 138) /*yellow*/ },

            { "Ritual", Color.FromArgb(255, 157, 181, 244) /*light blue*/ },

            { "Fusion", Color.FromArgb(255, 160, 134, 183) /*violet*/ },

            { "Synchro", Color.FromArgb(255, 204, 204, 204) /*white*/ },
    
            { "XYZ", Color.FromArgb(255, 0, 0, 0) /*black*/ },

            { "Link", Color.FromArgb(255, 0, 0, 139) /*dark blue*/ },

            { "Effect", Color.FromArgb(255, 255, 139, 83) /*orange to brown*/ },

            { "Spell", Color.FromArgb(255, 29, 158, 116) /*green*/ },

            { "Trap", Color.FromArgb(255, 188, 90, 132) /*purple*/ },

            { "Skill", Color.FromArgb(255, 17, 126, 250) /*azure*/ }
        };

        public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string[] frameType)
            {
                LinearGradientBrush brush = new()
                                            {
                                                StartPoint = new Point(0, 0),
                                                EndPoint = new Point(0, 1),
                                            };

                Color color1 = FrameColors.Single(f => frameType.Any(t => t.Equals(f.Key, StringComparison.OrdinalIgnoreCase)))                                 .Value;
                Color color2;

                if (frameType.Any(t => t.Equals("Pendulum", StringComparison.OrdinalIgnoreCase)))
                {
                    color2 = FrameColors["Spell"];
                    brush.GradientStops = new() { new GradientStop(color1, 0.35), new GradientStop(color2, 0.65) };
                }

                else
                {
                    color2 = Color.FromArgb((byte)(color1.A * 0.6), color1.R, color1.G, color1.B);
                    brush.GradientStops = new() { new GradientStop(color2, 0), new GradientStop(color1, 1) };
                }

                return brush;
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }
}

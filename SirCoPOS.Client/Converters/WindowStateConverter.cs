using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace SirCoPOS.Client.Converters
{
    class WindowStateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var enabled = ((bool?)value) ?? false;
            if (enabled)
                return Xceed.Wpf.Toolkit.WindowState.Open;
            else
                return Xceed.Wpf.Toolkit.WindowState.Closed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var state = (Xceed.Wpf.Toolkit.WindowState)value;
            if (state == Xceed.Wpf.Toolkit.WindowState.Open)
                return true;
            else
                return false;
        }
    }
}

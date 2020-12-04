using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace SirCoPOS.Client.Converters
{
    class ValueVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is decimal)
            {
                var num = (decimal?)value;
                if ((num ?? 0) != 0)
                    return Visibility.Visible;
            }
            if (value is int)
            {
                var num = (int?)value;
                if ((num ?? 0) != 0)
                    return Visibility.Visible;
            }
            var prm = (string)parameter;
            if (prm == "collapse")
                return Visibility.Collapsed;
            return Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

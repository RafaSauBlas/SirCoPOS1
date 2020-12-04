using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace SirCoPOS.Client.Converters
{
    public class ImageUrlConverter : IValueConverter
    {
        public ImageUrlConverter()
        {

        }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string imgurl = null;
            if (GalaSoft.MvvmLight.ViewModelBase.IsInDesignModeStatic)
            {
                return "/SirCoPOS.Win;component/Images/Vendedora.png";
            }
            else
            {
                imgurl = ConfigurationManager.AppSettings["baseUrl"] + ConfigurationManager.AppSettings[(string)parameter];
            }

            if (value == null)
                return null;

            var url = String.Format(imgurl, value);
            return url;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

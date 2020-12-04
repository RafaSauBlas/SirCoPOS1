using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace SirCoPOS.Client.Converters
{
    class ImageUrlMultiConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
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

            if (values == null || !values.Any())
                return null;

            var url = String.Format(imgurl, values);
            //var ic = new System.Windows.Media.ImageSourceConverter();
            //var res = ic.ConvertFrom(url);

            var bi = new BitmapImage();
            bi.BeginInit();
            bi.UriSource = new Uri(url);
            bi.EndInit();
            return bi;
            //return url;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

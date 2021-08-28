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
            string marcamodelo;
            string baseUrl = "http://201.148.82.174/FOTOS/";
            string imgurl = null;
            string url =null; 

            if (GalaSoft.MvvmLight.ViewModelBase.IsInDesignModeStatic)
            {
                return "/SirCoPOS.Win;component/Images/Vendedora.png";
            }
            else
            {
                if (value == null)
                    return null;
                switch (parameter)
                {
                    case "ProductoUrl":

                        if ( value.GetType() ==typeof(string) )
                        {
                            marcamodelo = (string)value;
                            imgurl = baseUrl + marcamodelo.Replace(' ', '_') + "F1.png";
                            url = String.Format(imgurl);
                        }
                        break;
                    case "EmpleadoUrl":
                        if (value.GetType() == typeof(int))
                        {
                            imgurl = ConfigurationManager.AppSettings["baseUrl"] + ConfigurationManager.AppSettings[(string)parameter];
                            url = String.Format(imgurl, value);
                        }
                        break;
                    default:
                        break;
                }

            }
            if (imgurl ==null) {
                return null;
            }
            return url;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

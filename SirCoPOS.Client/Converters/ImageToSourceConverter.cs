using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SirCoPOS.Client.Converters
{
    public class ImageToSourceConverter : IValueConverter
    {
        public ImageToSourceConverter()
        {
        }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
          if (targetType == typeof(ImageSource))
          {
           if (value is SirCoPOS.Common.Constants.FormaPago)
            {
              string str  = "/SirCoPOS.Win;component/Images/" + value.ToString() + ".png";
              return new BitmapImage(new Uri(str, UriKind.RelativeOrAbsolute));
            }
            else if (value is Uri)
            {
             Uri uri = (Uri)value;
             return new BitmapImage(uri);
            }
          }
          return value;
         }
     
         public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
         {
            throw new NotImplementedException();
         }
    }
}
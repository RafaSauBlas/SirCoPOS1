using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
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
        public string FTP = "http://201.148.82.174/FOTOS/";
        public string IPP = @"\\10.10.1.1\Sistema\ZT\Fotos\";
        string marcamodelo;
        string imgurl;
        string imgpath;
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
           else if (value is string)
            {
                    marcamodelo = (string)value;
                    imgurl = FTP + marcamodelo.Replace(' ', '_') + "F3.png";
                    imgpath = IPP + marcamodelo.Replace(' ', '_') + "F1.jpg";

                    BitmapImage imagen = new BitmapImage(new Uri(imgurl));
                    if (imagen.CanFreeze == false)
                    {
                        if (!File.Exists(imgpath))
                        {
                            imagen = null;
                        }
                        else
                        {
                            imagen = new BitmapImage(new Uri(imgpath));
                        }
                    }
                    return imagen;
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
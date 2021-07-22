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
    public class FPagoToTextConverter : IValueConverter
    {
        public FPagoToTextConverter()
        {
        }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
          if (targetType == typeof(String))
          {
           if (value is SirCoPOS.Common.Constants.FormaPago)
            {
                switch (value)
                {
                    case SirCoPOS.Common.Constants.FormaPago.CD:
                        return "CREDITO DISTRIBUIDOR";
                        break;
                    case SirCoPOS.Common.Constants.FormaPago.CP:
                        return "CREDITO PERSONAL";
                        break;
                    case SirCoPOS.Common.Constants.FormaPago.CV:
                        return "CONTRAVALE";
                        break;
                    case SirCoPOS.Common.Constants.FormaPago.DV:
                        return "DEVOLUCION";
                        break;
                    case SirCoPOS.Common.Constants.FormaPago.EF:
                        return "EFECTIVO";
                        break;
                    case SirCoPOS.Common.Constants.FormaPago.TC:
                        return "TARJETA DE CREDITO";
                        break;
                    case SirCoPOS.Common.Constants.FormaPago.TD:
                        return "TARJETA DE DEBITO";
                        break;
                    case SirCoPOS.Common.Constants.FormaPago.VA:
                        return "VALE";
                        break;
                    case SirCoPOS.Common.Constants.FormaPago.VD:
                        return "VALE DIGITAL";
                        break;
                    case SirCoPOS.Common.Constants.FormaPago.MD:
                        return "MONEDERO ELECTRONICO";
                        break;
                    case SirCoPOS.Common.Constants.FormaPago.VE:
                        return "VALE EXTERNO";
                        break;
                    default:
                        return "";
                        break;
                }

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
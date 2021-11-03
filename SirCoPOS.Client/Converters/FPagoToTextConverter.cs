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
                        return "Ctrl + O - CRÉDITO DISTRIBUIDOR";
                    case SirCoPOS.Common.Constants.FormaPago.CP:
                        return "Ctrl + P - CRÉDITO PERSONAL";
                    case SirCoPOS.Common.Constants.FormaPago.CV:
                        return "Ctrl + E - CONTRAVALE";
                    case SirCoPOS.Common.Constants.FormaPago.DV:
                        return "Ctrl + D - DEVOLUCIÓN";
                    case SirCoPOS.Common.Constants.FormaPago.EF:
                        return "Ctrl + F - EFECTIVO";
                    case SirCoPOS.Common.Constants.FormaPago.TC:
                        return "Ctrl + R - TARJETA DE CRÉDITO";
                    case SirCoPOS.Common.Constants.FormaPago.TD:
                        return "Ctrl + T - TARJETA DE DEBITO";
                    case SirCoPOS.Common.Constants.FormaPago.VA:
                        return "Ctrl + V - VALE";
                    case SirCoPOS.Common.Constants.FormaPago.VD:
                        return "Ctrl + I - VALE DIGITAL";
                    case SirCoPOS.Common.Constants.FormaPago.MD:
                        return "Ctrl + M - MONEDERO ELECTRÓNICO";
                    case SirCoPOS.Common.Constants.FormaPago.VE:
                        return "Ctrl + E - VALE EXTERNO";
                    case SirCoPOS.Common.Constants.FormaPago.GO:
                        return "Ctrl + G - GO PLAZOS";
                    case SirCoPOS.Common.Constants.FormaPago.KP:
                        return "Ctrl + K - KUESKI-PAY";
                        default:
                        return "";
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
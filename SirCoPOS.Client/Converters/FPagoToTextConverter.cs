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
                        return "F9-CR�DITO DISTRIBUIDOR";
                    case SirCoPOS.Common.Constants.FormaPago.CP:
                        return "F8-CR�DITO PERSONAL";
                    case SirCoPOS.Common.Constants.FormaPago.CV:
                        return "F7-CONTRAVALE";
                    case SirCoPOS.Common.Constants.FormaPago.DV:
                        return "F4-DEVOLUCI�N";
                    case SirCoPOS.Common.Constants.FormaPago.EF:
                        return "F1-EFECTIVO";
                    case SirCoPOS.Common.Constants.FormaPago.TC:
                        return "F2-TARJETA DE CR�DITO";
                    case SirCoPOS.Common.Constants.FormaPago.TD:
                        return "F3-TARJETA DE DEBITO";
                    case SirCoPOS.Common.Constants.FormaPago.VA:
                        return "F6-VALE";
                    case SirCoPOS.Common.Constants.FormaPago.VD:
                        return "F10-VALE DIGITAL";
                    case SirCoPOS.Common.Constants.FormaPago.MD:
                        return "F12-MONEDERO ELECTR�NICO";
                    case SirCoPOS.Common.Constants.FormaPago.VE:
                        return "F11-VALE EXTERNO";
                    case SirCoPOS.Common.Constants.FormaPago.GO:
                        return "F5-GO PLAZOS";
                    case SirCoPOS.Common.Constants.FormaPago.KU:
                        return "KUESKI-PAY";
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
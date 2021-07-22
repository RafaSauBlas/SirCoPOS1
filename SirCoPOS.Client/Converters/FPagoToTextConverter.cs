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
                    case SirCoPOS.Common.Constants.FormaPago.CP:
                        return "CREDITO PERSONAL";
                    case SirCoPOS.Common.Constants.FormaPago.CV:
                        return "CONTRAVALE";
                    case SirCoPOS.Common.Constants.FormaPago.DV:
                        return "DEVOLUCION";
                    case SirCoPOS.Common.Constants.FormaPago.EF:
                        return "EFECTIVO";
                    case SirCoPOS.Common.Constants.FormaPago.TC:
                        return "TARJETA DE CREDITO";
                    case SirCoPOS.Common.Constants.FormaPago.TD:
                        return "TARJETA DE DEBITO";
                    case SirCoPOS.Common.Constants.FormaPago.VA:
                        return "VALE";
                    case SirCoPOS.Common.Constants.FormaPago.VD:
                        return "VALE DIGITAL";
                    case SirCoPOS.Common.Constants.FormaPago.MD:
                        return "MONEDERO ELECTRONICO";
                    case SirCoPOS.Common.Constants.FormaPago.VE:
                        return "VALE EXTERNO";
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
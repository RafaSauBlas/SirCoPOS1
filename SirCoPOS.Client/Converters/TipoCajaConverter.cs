using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace SirCoPOS.Client.Converters
{
    class TipoCajaConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var caja = (Common.Entities.Caja)value;
            if (caja == null)
                return null;
            switch (caja.Tipo)
            {
                case (byte)Common.Constants.TipoFondo.Cajon:
                    return $"Cajon {caja.Numero}";
                case (byte)Common.Constants.TipoFondo.CajaFuerte:
                    return $"CajaFuerte {caja.Numero}";
                default:
                    return $"{caja.Tipo}-{caja.Numero}";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

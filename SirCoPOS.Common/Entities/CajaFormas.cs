using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Common.Entities
{
    public class CajaFormas
    {
        public decimal Efectivo { get; set; }
        public IEnumerable<CajaFormaPago> FormasPago { get; set; }
    }
    public class CajaFormaPago
    {
        public Constants.FormaPago FormaPago { get; set; }
        public int Unidades { get; set; }
        public decimal Monto { get; set; }
    }
}

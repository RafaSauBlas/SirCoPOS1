using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Reports.Entities
{
    public class Pago
    {
        public string FormaPago { get; set; }
        public decimal Importe { get; set; }
        public decimal Efectivo { get; set; }
        public string Referencia { get; set; }
        public string Folio { get; set; }
    }
}

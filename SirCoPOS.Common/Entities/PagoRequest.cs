using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Common.Entities
{
    public class PagoRequest
    {
        public string Distribuidor { get; set; }
        public string Sucursal { get; set; }
        public decimal Importe { get; set; }
        public int Cajero { get; set; }
    }
}

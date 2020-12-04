using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Common.Entities
{
    public class CorteResponse
    {
        public decimal Importe { get; set; }
        public IEnumerable<SeriePrecio> Series { get; set; }
        public IEnumerable<FormaPagoCorte> FormaPagoTotales { get; set; }
    }
}

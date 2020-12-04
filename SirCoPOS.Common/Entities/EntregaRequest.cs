using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Common.Entities
{
    public class EntregaRequest
    {
        public string Sucursal { get; set; }
        public int CajeroId { get; set; }
        public int AuditorId { get; set; }
        public decimal Entregar { get; set; }
        public IEnumerable<EntregaFormaPago> FormasPago { get; set; }
    }
    public class EntregaFormaPago
    { 
        public Common.Constants.FormaPago FormaPago { get; set; }
        public int Entregar { get; set; }
        public decimal Amount { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Common.Entities
{
    public class RequestCorte
    {
        public int CajeroId { get; set; }
        public int AuditorId { get; set; }
        public decimal Reportado { get; set; }
        public IEnumerable<string> Series { get; set; }
        public IEnumerable<RequestCorteFormaPago> FormasPago { get; set; }
    }

    public class RequestCorteFormaPago
    {
        public int FormaPago { get; set; }
        public int Reportado { get; set; }
    }    
}

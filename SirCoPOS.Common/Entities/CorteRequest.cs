using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Common.Entities
{
    public class CorteRequest
    {
        public string Sucursal { get; set; }
        public int CajeroId { get; set; }
        public int AuditorId { get; set; }        
        public decimal Entregar { get; set; }        
        public IEnumerable<Common.Entities.ItemCorte> FormasPago { get; set; }
        public IEnumerable<string> Series { get; set; }
    }

}

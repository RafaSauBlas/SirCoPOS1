using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Common.Entities
{
    public class ReturnRequest
    {
        public string Sucursal { get; set; }
        public string Folio { get; set; }
        public IEnumerable<string> Items { get; set; }
        public string Comments { get; set; }
        public Cliente Cliente { get; set; }
        public IDictionary<string, RazonItem> Razones { get; set; }
    }
    public class RazonItem
    { 
        public int TipoRazon { get; set; }
        public string Notas { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Common.Entities
{
    public class ChangeRequest
    {
        public int VendedorId { get; set; }
        public string Sucursal { get; set; }
        public string Folio { get; set; }
        public IEnumerable<ChangeItem> Items { get; set; }
        public Cliente Cliente { get; set; }
        public IEnumerable<Pago> Pagos { get; set; }
        public IDictionary<string, RazonItem> Razones { get; set; }
    }
}

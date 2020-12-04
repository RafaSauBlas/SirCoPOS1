using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Common.Entities
{
    public class VentaView
    {
        public string Sucursal { get; set; }
        public string Folio { get; set; }
        public IEnumerable<ProductoView> Productos { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Common.Entities
{
    public class DevolucionResponse
    {
        public string Sucursal { get; set; }
        public string Folio { get; set; }
        public int? CajeroId { get; set; }
        public string CajeroNombre { get; set; }
        public int? VendedorId { get; set; }
        public int? ClienteId { get; set; }        
        public string VendedorNombre { get; set; }
        public IEnumerable<ProductoSale> Productos { get; set; }
    }
}

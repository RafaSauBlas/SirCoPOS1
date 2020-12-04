using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Reports.Entities
{
    public class ReciboDevolucion
    {
        public string SucursalId { get; set; }
        public string SucursalNombre { get; set; }
        public string Folio { get; set; }
        public DateTime Fecha { get; set; }
        public DateTime FechaVenta { get; set; }
        public string SucursalVenta { get; set; }
        public string FolioVenta { get; set; }
        public string CajeroId { get; set; }
        public string CajeroNombre { get; set; }
        public string VendedorId { get; set; }
        public string VendedorNombre { get; set; }
        public string Observaciones { get; set; }

    }
}

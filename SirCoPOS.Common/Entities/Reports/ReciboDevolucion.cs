using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Common.Entities.Reports
{
    public class ReciboDevolucionReport
    { 
        public ReciboDevolucion Recibo { get; set; }
        public IEnumerable<Producto> Productos { get; set; }
        public IEnumerable<Pago> Pagos { get; set; }
        public IEnumerable<DevolObservacion> Observaciones { get; set; }
    }
    public class ReciboDevolucion
    {
        public string SucursalId { get; set; }
        public string SucursalNombre { get; set; }
        public string Direccion { get; set; }
        public string Colonia { get; set; }
        public string Folio { get; set; }
        public DateTime Fecha { get; set; }
        public DateTime FechaVenta { get; set; }
        public string SucursalVenta { get; set; }
        public string FolioVenta { get; set; }
        public string CajeroId { get; set; }
        public string CajeroNombre { get; set; }
        public string VendedorId { get; set; }
        public string VendedorNombre { get; set; }
        public decimal? Disponible { get; set; }
        public string Observaciones { get; set; }        
    }

}

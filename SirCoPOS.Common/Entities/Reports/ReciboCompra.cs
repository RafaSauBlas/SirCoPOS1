using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Common.Entities.Reports
{
    public class ReciboCompraReport
    {
        public ReciboCompra Recibo { get; set; }
        public IEnumerable<Producto> Productos { get; set; }
        public IEnumerable<Pago> Pagos { get; set; }
        public IEnumerable<PlanPago> PlanPagos { get; set; }
    }
    public class ReciboCompra
    {
        public string RFC { get; set; }
        public string SucursalId { get; set; }
        public string SucursalNombre { get; set; }
        public string Direccion { get; set; }
        public string Colonia { get; set; }
        public string VendedorId { get; set; }
        public string VendedorNombre { get; set; }
        public string CajeroId { get; set; }
        public string CajeroNombre { get; set; }
        public string Folio { get; set; }
        public decimal? Efectivo { get; set; }
        public string CantidadLetra { get; set; }
        public decimal? Descuento { get; set; }
        public DateTime Cambio { get; set; }
        public DateTime Fecha { get; set; }        
        public decimal? Dinero { get; set; }
    }
    public class Pago
    {
        public string FormaPago { get; set; }
        public decimal Importe { get; set; }
        public string Referencia { get; set; }
        public string Folio { get; set; }
    }
    public class PlanPago
    { 
        public int Pago { get; set; }  
        public DateTime Date { get; set; }
        public decimal Importe { get; set; }
    }
}

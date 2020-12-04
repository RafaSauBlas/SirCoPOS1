using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SirCoPOS.Web.Areas.Admin.Models
{
    public class Venta
    {
        public string Sucursal { get; set; }
        public string Folio { get; set; }
        public DateTime? Fecha { get; set; }
        public IEnumerable<Producto> Detalle { get; set; }
        public string Estatus { get; set; }
        public Persona Cajero { get; set; }
        public Persona Vendedor { get; set; }
        public Pago Pago { get; set; }
        public IEnumerable<PlanPago> PlanPago { get; set; }
        public Persona Cliente { get; internal set; }
        public IEnumerable<ContraVale> ContraVales { get; set; }
        public IEnumerable<DevolucionVenta> Devoluciones { get; set; }
    }

}
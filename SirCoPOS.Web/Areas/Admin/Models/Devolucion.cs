using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SirCoPOS.Web.Areas.Admin.Models
{
    public class Devolucion
    {
        public string Sucursal { get; set; }
        public string Folio { get; set; }
        public DateTime? Fecha { get; set; }
        public IEnumerable<Producto> Detalle { get; set; }
        public string Estatus { get; set; }
        public decimal? Total { get; internal set; }
        public decimal? Disponible { get; internal set; }
        public string VentaSucursal { get; set; }
        public string VentaFolio { get; set; }
        public IEnumerable<DevolucionVenta> Ventas { get; set; }
        public Persona Cliente { get; internal set; }
    }
}
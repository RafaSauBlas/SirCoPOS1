using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SirCoPOS.Web.Areas.Admin.Models
{
    public class Pago
    {
        public DateTime? Fecha { get; set; }
        public string Estatus { get; set; }
        public IEnumerable<PagoDetalle> Detalle { get; set; }        
        public Venta Venta { get; set; }
    }

    public class PagoDetalle
    {
        public int FormaPago { get; set; }
        public decimal? Importe { get; set; }
        public string Sucursal { get; set; }
        public string Folio { get; set; }
        public string Terminacion { get; set; }
        public string Transaccion { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SirCoPOS.Web.Areas.Admin.Models
{
    public class DevolucionVenta
    {
        public string Sucursal { get; set; }
        public string Folio { get; set; }
        public decimal? Importe { get; set; }
    }
}
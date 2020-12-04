using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SirCoPOS.Web.Areas.Admin.Models
{
    public class PlanPagoDetalle
    {
        public long Number { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Importe { get; set; }
    }
}
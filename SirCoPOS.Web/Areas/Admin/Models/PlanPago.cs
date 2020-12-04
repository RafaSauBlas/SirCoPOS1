using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SirCoPOS.Web.Areas.Admin.Models
{
    public class PlanPago
    {
        public IEnumerable<PlanPagoDetalle> Detalle { get; set; }
        public string Vale { get; set; }
    }
}
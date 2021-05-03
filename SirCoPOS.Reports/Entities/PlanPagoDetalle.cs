using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Reports.Entities
{
    public class PlanPagoDetalle
    {
        public int Pago { get; set; }
        public DateTime FechaAplicar { get; set; }
        public decimal Importe { get; set; }
    }
}




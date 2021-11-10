using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Common.Entities
{
    public abstract class PromocionesResponse
    {
        //public IEnumerable<int> Plazos { get; set; }
        public int PagosMax { get; set; }
        public int PagosMin { get; set; }
        public int Selected { get; set; }
    }

    public class PromocionesValeResponse : PromocionesResponse
    {
        public IEnumerable<DateTime> Promociones { get; set; }
        public IEnumerable<DateTime> Fechas { get; set; }
        public decimal Blindaje { get; set; }
    }

    public class PromocionesCreditoResponse : PromocionesResponse
    {
        public DateTime? Promocion { get; set; }        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Common.Entities
{
    public class SaleResponse
    {
        public string Folio { get; set; }
        public int? Cliente { get; set; }
        public decimal? Monedero { get; set; }
        public IEnumerable<ContraValeResponse> ContraVales { get; set; }
        public bool Multiple { get; set; }
    }

    public class ContraValeResponse
    {
        public string Vale { get; set; }
        public string ContraVale { get; set; }
        public decimal Importe { get; set; }
        public DateTime Caducidad { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Common.Entities
{
    public class SerieFormasPago
    {
        public int? ArticuloId { get; set; }
        public string Serie { get; set; }
        public decimal? Precio { get; set; }
        public IEnumerable<Constants.FormaPago> FormasPago { get; set; }
        public IDictionary<Constants.FormaPago, decimal?> Pagos { get; set; }
        public IDictionary<Constants.FormaPago, bool> Promociones { get; set; }
        //public int? Plazos { get; set; }
        public int? AdicionalId { get; set; }
        public string AdicionalDesc { get; set; }
        public string Notas { get; set; }
        public int? NotaRazon { get; set; }
    }
    
}

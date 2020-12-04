using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Common.Entities
{
    public class CheckPromocionesRequest
    {
        public string Sucursal { get; set; }
        public IEnumerable<SerieFormasPago> Productos { get; set; }        
    }

    public class CheckPromocionesCuponesRequest : CheckPromocionesRequest
    {
        public IEnumerable<PromocionCuponItem> PromocionesCupones { get; set; }
        public bool HasCliente { get; set; }
        public int? ClienteId { get; set; }
    }

    public class CheckPromocionesCuponesResponse
    {
        public decimal? Monedero { get; set; }
        public IEnumerable<Common.Entities.ProductoPromocion> Promociones { get; set; }
    }
}

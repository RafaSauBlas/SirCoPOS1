using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Common.Entities
{
    public class ProductoPromocion
    {
        public string Serie { get; set; }
        public decimal? Descuento { get; set; }
        public decimal? Fijo { get; set; }
        public int? PromocionId { get; set; }        
        public bool? Promo { get; set; }
        public decimal? Monedero { get; set; }
        public byte? Index { get; set; }
        public override string ToString()
        {
            if (this.PromocionId.HasValue)
            {                
                return $"{this.Serie} - PID: {this.PromocionId}({this.Index}), Desc: {this.Descuento:c}, Fijo: {this.Fijo:c}, Mon: {this.Monedero:c}, Promo: {this.Promo} ";
            }
            return $"{this.Serie}";
        }
    }
}

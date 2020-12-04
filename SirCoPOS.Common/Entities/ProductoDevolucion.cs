using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Common.Entities
{
    public class ProductoDevolucion
    {
        public int Id { get; set; }
        public string Sucursal { get; set; } 
        public string Folio { get; set; }
        public string Serie { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Talla { get; set; }
        public string Corrida { get; set; }
        public decimal? Precio { get; set; }
        public decimal? Pago { get; set; }
        public decimal? DescuentoPorcentaje
        {
            get {
                if (this.Precio != this.Pago)
                {
                    var desc = this.Precio - this.Pago;
                    var per = desc / this.Precio;
                    return per;
                }
                return null;
            }
        }
    }
}

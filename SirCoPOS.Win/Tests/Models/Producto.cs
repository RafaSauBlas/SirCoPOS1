using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Win.Tests.Models
{
    class Producto
    {
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Talla { get; set; }
        public decimal Precio { get; set; }
        public decimal Descuento { get; set; }
        public decimal Total { get { return this.Precio + this.Descuento; } }
    }
}

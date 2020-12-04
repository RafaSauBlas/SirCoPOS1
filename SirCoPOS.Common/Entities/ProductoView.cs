using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Common.Entities
{
    public class ProductoView
    {
        public int ArticuloId { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Medida { get; set; }
        public string Serie { get; set; }
        public decimal? Precio { get; set; }
    }
}

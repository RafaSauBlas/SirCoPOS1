using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Reports.Entities
{
    public class Producto
    {
        public string Serie { get; set; }
        public decimal Precio { get; set; }
        public decimal Importe { get; set; }
        public string Marca { get; set; }
        public string Descripcion { get; set; }
        public string Estilo { get; set; }
        public string Medida { get; set; }
    }
}

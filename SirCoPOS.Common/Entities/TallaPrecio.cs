using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Common.Entities
{
    public class TallaPrecio
    {
        public string Corrida { get; set; }
        public string MedidaInicio { get; set; }
        public string MedidaFin { get; set; }
        public decimal? Precio { get; set; }
    }
}

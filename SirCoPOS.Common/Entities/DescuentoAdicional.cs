using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Common.Entities
{
    public class DescuentoAdicional
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public decimal Descuento { get; set; }
        public bool Devolucion { get; set; }
    }
}

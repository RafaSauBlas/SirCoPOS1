using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Common.Entities
{
    public class Caja
    {
        public byte Tipo { get; set; }
        public byte Numero { get; set; }
        public decimal Importe { get; set; }
    }
}

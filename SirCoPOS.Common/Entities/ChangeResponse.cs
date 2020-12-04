using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Common.Entities
{
    public class ChangeResponse
    {
        public string Devolucion { get; set; }
        public string Venta { get; set; }
        public int? Cliente { get; set; }
    }
}

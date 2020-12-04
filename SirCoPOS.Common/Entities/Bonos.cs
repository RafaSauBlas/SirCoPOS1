using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Common.Entities
{
    public class Bonos
    {
        public DateTime? Pagado { get; set; }
        public int? EmisorId { get; set; }
        public IEnumerable<BonoDetalle> Detalle { get; set; }
    }
    public class BonoDetalle
    {
        public decimal Unidades { get; set; }
        public string Descripcion { get; set; }
        public decimal Importe { get; set; }
    }
}

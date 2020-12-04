using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Common.Entities
{
    public class MedidasCorridas
    {
        public IEnumerable<Common.Entities.TallaPrecio> Corridas { get; set; }
        public IEnumerable<string> Medidas { get; set; }
    }
}

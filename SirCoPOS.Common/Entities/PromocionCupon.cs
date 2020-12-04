using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Common.Entities
{
    public class PromocionCupon : Promocion
    {
        public override bool IsCupon => true;
        public int CuponId { get; set; }
        public string Cupon { get; set; }
        public string Descripcion { get; set; }
        public string Restricciones { get; set; }
        public int? Cliente { get; set; }        
    }
}

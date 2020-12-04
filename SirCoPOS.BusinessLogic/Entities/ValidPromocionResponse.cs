using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.BusinessLogic.Entities
{
    class ValidPromocionResponse
    {
        public bool IsValid { get; set; }
        public List<Entities.ProductoPromocion> ValidPromo { get; set; }
        public List<Entities.ProductoPromocion> ValidCompra { get; set; }
    }
}

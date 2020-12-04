using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Common.Entities
{
    public class FormaPagoImporte
    {
        public Guid ID { get; set; }
        public Constants.FormaPago FormaPago { get; set; }
        public decimal? Importe { get; set; }
        public bool HasPromocion { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Common.Entities
{
    public class CancelSaleRequest
    {
        public string Sucursal { get; set; }
        public string Folio { get; set; }
        public string Motivo { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace SirCoPOS.Contracts.Entities
{
    public class SaleRequest
    {
        public string Sucursal { get; set; }
        public IEnumerable<string> Series { get; set; }
        public decimal? Pagar { get; set; }
    }

    public class SaleResponse
    { 
        public string Folio { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Common.Entities
{
    public class ContraVale
    {
        public string Sucursal { get; set; }
        public string Folio { get; set; }
        public int? Cliente { get; set; }
        public decimal? Importe { get; set; }
        public Distribuidor Distribuidor { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.BusinessLogic.Entities
{
    public class PromocionValores
    {
        public bool Success { get; set; }
        public decimal? DescuentoPorcentaje { get; set; }
        public decimal? DescuentoFijo { get; set; }
        public decimal? DescuentoImporteFijo { get; set; }
        public decimal? MonederoPorcentaje { get; set; }
        public decimal? MonederoFijo { get; set; }
    }
}

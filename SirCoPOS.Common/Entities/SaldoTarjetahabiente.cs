using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Common.Entities
{
    public class SaldoTarjetahabiente
    {
        public int DistribuidorId { get; set; }
        public string Nombre { get; set; }
        public decimal? Vencido { get; set; }
        public decimal? Periodo { get; set; }
        public decimal? PorVencer { get; set; }
        public decimal? Total { get; set; }
    }
}

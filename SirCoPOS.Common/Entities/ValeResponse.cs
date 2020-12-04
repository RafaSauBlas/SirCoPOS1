using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Common.Entities
{
    public class ValeResponse
    {
        public string Vale { get; set; }
        public decimal Disponible { get; set; }
        public bool Cancelado { get; set; }
        public string CanceladoMotivo { get; set; }
        public Distribuidor Distribuidor { get; set; }
        public int? ClienteId { get; set; }
        public DateTime? Vigencia { get; set; }
        public bool WithLimite { get; set; }
        public decimal? Limite { get; set; }
    }
    public class CValeResponse : ValeResponse
    { 
        public string Sucursal { get; set; }
    }
}

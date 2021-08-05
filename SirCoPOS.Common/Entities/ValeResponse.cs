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
        public bool Usado { get; set; }
        public string SucursalUsado { get; set; }
        public string NotaUsado { get; set; }
        public DateTime FechaUsado { get; set; }
    }
    public class CValeResponse : ValeResponse
    { 
        public string Sucursal { get; set; }
        public string Venta { get; set; }
        public decimal? Saldo { get; set; }
    }
    public class DistribuidorObserva
    {
        public string Observa01 { get; set; }
        public string Observa02 { get; set; }
        public string Observa03 { get; set; }
        public string Observa04 { get; set; }
        public string Observa05 { get; set; }
    }

}

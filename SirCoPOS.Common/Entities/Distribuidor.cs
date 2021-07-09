using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Common.Entities
{
    public class Distribuidor
    {
        public int Id { get; set; }
        public string Distrib { get; set;}
        public string Cuenta { get; set; }
        public string Nombre { get; set; }
        public string ApPaterno { get; set; }
        public string ApMaterno { get; set; }
        public Common.Constants.StatusDistribuidor Status { get; set; }
        public bool Electronica { get; set; }        
        public IEnumerable<short> Firmas { get; set; }
        public bool ContraVale { get; set; }
        public bool ValeExterno { get; set; }
        public bool Promocion { get; set; }
        public decimal? Disponible { get; set; }
        public short Plazos { get; set; }
        public string Number { get; set; }
        public DateTime? Date { get; set; }
        public int? ClienteId { get; set; }
        public decimal? LimiteVale { get; set; }
    }
}

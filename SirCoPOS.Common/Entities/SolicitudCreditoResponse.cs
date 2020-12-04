using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Common.Entities
{
    public class SolicitudCreditoResponse
    {
        public Guid Id { get; set; }
        public bool? Processing { get; set; }
        public decimal? Monto { get; set; }
        public bool? Electronica { get; set; } 
    }
}

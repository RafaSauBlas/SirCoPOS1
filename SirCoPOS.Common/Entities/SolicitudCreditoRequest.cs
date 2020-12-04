using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Common.Entities
{
    public class SolicitudCreditoRequest
    {
        public string Sucursal { get; set; }
        public string Vale { get; set; }
        public decimal Monto { get; set; }
        public bool Electronica { get; set; }
        public int idusuario { get; set; }
    }
}

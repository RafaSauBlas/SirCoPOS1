using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Client.Models.Pagos
{
    public class PagoTarjeta : Pago
    {
        public string Terminacion { get; set; }
        public string Referencia { get; set; }    
    }
}

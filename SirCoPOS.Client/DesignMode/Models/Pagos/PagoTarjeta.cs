using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Client.DesignMode.Models.Pagos
{
    class PagoTarjeta : Client.Models.Pagos.PagoTarjeta
    {
        public PagoTarjeta()
        {
            this.FormaPago = Common.Constants.FormaPago.TC;
            this.Importe = 789m;
            this.Referencia = "4456";
            this.Terminacion = "999";
        }
    }
}

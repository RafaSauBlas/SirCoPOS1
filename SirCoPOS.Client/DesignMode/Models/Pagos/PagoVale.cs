using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Client.DesignMode.Models.Pagos
{
    class PagoVale : Client.Models.Pagos.PagoVale
    {
        public PagoVale()
        {
            this.FormaPago = Common.Constants.FormaPago.VA;
            this.Importe = 1999.50m;
            this.Vale = "B0000001";
            this.Info = new Client.Models.Pagos.PagoValeInfo 
            {
                Distribuidor = "distribuidor nombre",
                Electronica = false
            };
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SirCoPOS.Common.Constants;

namespace SirCoPOS.Client.ViewModels.Caja
{
    class PagoTarjetaDebitoViewModel : PagoTarjetaCreditoViewModel
    {
        public override string Title => "Pago Tarjeta Débito";
        public override FormaPago FormaPago => FormaPago.TD;
    }
}

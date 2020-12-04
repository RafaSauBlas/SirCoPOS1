using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Win.Tests.ViewModels
{
    class PagosViewModel
    {
        public PagosViewModel()
        {
            this.Pagos = new ObservableCollection<Models.PagoBase>();
            this.Pagos.Add(new Models.PagoEfectivo { Importe = 123 });
            this.Pagos.Add(new Models.PagoTarjeta { Importe = 456, Terminacion = "789", Referencia = "777" });

            //this.Pagos.Add(new Models.PagoBase { Importe = 1 });
            //this.Pagos.Add(new Models.PagoBase { Importe = 2 });
            //this.Pagos.Add(new Models.PagoBase { Importe = 3 });
            //this.Pagos.Add(new Models.PagoBase { Importe = 4 });
            //this.Pagos.Add(new Models.PagoBase { Importe = 5 });
            //this.Pagos.Add(new Models.PagoBase { Importe = 6 });
        }
        public ObservableCollection<Models.PagoBase> Pagos { get; private set; }
    }
}

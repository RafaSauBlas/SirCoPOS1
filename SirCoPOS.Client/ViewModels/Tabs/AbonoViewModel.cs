using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Client.ViewModels.Tabs    
{
    class AbonoViewModel : Helpers.TabViewModelBase
    {
        public AbonoViewModel()
        {
            //if (this.IsInDesignMode)
            {
                this.Search = "0000003342601";
                this.Saldo = new Common.Entities.SaldoTarjetahabiente
                {                    
                    Nombre  = "nombre",
                    Vencido = 1000,
                    Periodo = 500,
                    PorVencer = 250,
                    Total = 1500
                };
                this.Pago = 123m;
            }
        }
        private string _search;
        public string Search
        {
            get => _search;
            set => this.Set(nameof(this.Search), ref _search, value);
        }
        private decimal? _pago;
        public decimal? Pago
        {
            get => _pago;
            set => this.Set(nameof(this.Pago), ref _pago, value);
        }
        private Common.Entities.SaldoTarjetahabiente _saldo;
        public Common.Entities.SaldoTarjetahabiente Saldo
        {
            get => _saldo;
            set => this.Set(nameof(this.Saldo), ref _saldo, value);
        }
    }
}

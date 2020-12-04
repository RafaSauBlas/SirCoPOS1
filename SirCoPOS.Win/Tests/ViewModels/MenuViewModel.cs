using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Win.Tests.ViewModels
{
    class MenuViewModel : GalaSoft.MvvmLight.ViewModelBase
    {
        public MenuViewModel()
        {
            this.Cajero = "Juan Antonio De La Fuente";
            this.Sucursal = "Triana";
            this.Fecha = DateTime.Now;

            var dt = new System.Windows.Threading.DispatcherTimer();
            dt.Tick += Dt_Tick;
            dt.Interval = TimeSpan.FromSeconds(1);
            dt.Start();
        }

        private void Dt_Tick(object sender, EventArgs e)
        {
            this.Fecha = DateTime.Now;
        }

        public string Cajero { get; set; }
        public string Sucursal { get; set; }
        private DateTime _fecha;
        public DateTime Fecha {
            get { return _fecha; }
            private set { this.Set(() => this.Fecha, ref _fecha, value); }
        }
    }
}

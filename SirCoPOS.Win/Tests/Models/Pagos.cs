using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Win.Tests.Models
{
    abstract class PagoBase : GalaSoft.MvvmLight.ObservableObject
    {
        private decimal _importe;
        public decimal Importe {
            get { return _importe; }
            set { this.Set(nameof(this.Importe), ref _importe, value); }
        }

        public string Nombre
        {
            get { return this.GetType().Name; }
        }
    }
    class PagoEfectivo : PagoBase
    {
        public PagoEfectivo()
        {
            if (GalaSoft.MvvmLight.ViewModelBase.IsInDesignModeStatic)
            {
                this.Importe = 123;
            }
        }
    }
    class PagoTarjeta : PagoBase
    {
        public PagoTarjeta()
        {
            if (GalaSoft.MvvmLight.ViewModelBase.IsInDesignModeStatic)
            {
                this.Importe = 456;
                this.Terminacion = "789";
                this.Referencia = "555";
            }
        }

        public string Terminacion { get; set; }
        public string Referencia { get; set; }
    }
    class PagoVale : PagoBase
    {
        public string Vale { get; set; }
        public int Plazos { get; set; }
        public DateTime Inicio { get; set; }
    }
    class PagoDevolucion : PagoBase
    {
        public string Sucursal { get; set; }
        public string Devolucion { get; set; }
    }
}

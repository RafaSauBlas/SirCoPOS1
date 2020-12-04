using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Utilities.Models
{
    public class Settings : GalaSoft.MvvmLight.ObservableObject
    {
        public Settings()
        {
            if (GalaSoft.MvvmLight.ViewModelBase.IsInDesignModeStatic)
            {
                this.Sucursal = new Common.Entities.Sucursal { Clave = "99" };
            }
        }
        private Common.Entities.Empleado _cajero;
        public Common.Entities.Empleado Cajero
        {
            get { return _cajero; }
            set { Set(() => this.Cajero, ref _cajero, value); }
        }
        private Common.Entities.Sucursal _sucursal;
        public Common.Entities.Sucursal Sucursal
        {
            get { return _sucursal; }
            set { this.Set(nameof(this.Sucursal), ref _sucursal, value); }
        }
        public bool MultiCaja { get; set; }
        public string Scanner { get; set; }
    }
}

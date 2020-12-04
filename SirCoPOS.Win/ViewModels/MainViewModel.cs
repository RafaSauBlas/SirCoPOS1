using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Win.ViewModels
{
    class MainViewModel : GalaSoft.MvvmLight.ViewModelBase
    {
        public MainViewModel()
        {            
            this.Fecha = DateTime.Now;
            if (this.IsInDesignMode)
            {
                this.Settings = new Utilities.Models.Settings
                {
                    Cajero = new Common.Entities.Empleado
                    {
                        Nombre = "nombre1",
                        ApellidoPaterno = "appaterno",
                        ApellidoMaterno = "apmaterno"
                    },
                    Sucursal = new Common.Entities.Sucursal
                    {
                        Clave = "01",
                        Descripcion = "Matriz"
                    }
                };                
            }
            else
            {
                this.Settings = CommonServiceLocator.ServiceLocator.Current.GetInstance<Utilities.Models.Settings>();

                var dt = new System.Windows.Threading.DispatcherTimer();
                dt.Tick += Dt_Tick;
                dt.Interval = TimeSpan.FromSeconds(1);
                dt.Start();
            }
        }

        private void Dt_Tick(object sender, EventArgs e)
        {
            this.Fecha = DateTime.Now;
        }
        private DateTime _fecha;
        public DateTime Fecha
        {
            get { return _fecha; }
            private set { this.Set(() => this.Fecha, ref _fecha, value); }
        }

        public Utilities.Models.Settings Settings { get; private set; }        
    }
}

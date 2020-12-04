using GalaSoft.MvvmLight.Messaging;
using SirCoPOS.Utilities.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Client.ViewModels.Devolucion
{
    class DevolucionItemViewModel : Helpers.ModalViewModelBase, Utilities.Interfaces.IModalDevolucionItem
    {
        private Common.ServiceContracts.IDataServiceAsync _data;
        public DevolucionItemViewModel()
        {
            this.PropertyChanged += DevolucionItemViewModel_PropertyChanged;
            if (!this.IsInDesignMode) {
                _data = CommonServiceLocator.ServiceLocator.Current.GetInstance<Common.ServiceContracts.IDataServiceAsync>();

                this.Options = _data.GetRazonesDevolucion();
            }
            if (this.IsInDesignMode)
            {
                this.Options = new Common.Entities.RazonNotaDevolucion[] {
                    new Common.Entities.RazonNotaDevolucion { Id = 1, Descripcion = "op1", Comentarios = false },
                    new Common.Entities.RazonNotaDevolucion { Id = 2, Descripcion = "op2", Comentarios = false },
                    new Common.Entities.RazonNotaDevolucion { Id = 3, Descripcion = "op3", Comentarios = false },
                };
                this.SelectedOpcion = this.Options.First();
                this.Item = new Common.Entities.ProductoDevolucion()
                {
                    Id = 1,
                    Sucursal = "suc",
                    Folio = "folio",
                    Serie = "serie",
                    Marca = "marca",
                    Modelo = "modelo",
                    Talla = "talla",
                    Corrida = "corrida",
                    Precio = 799m,
                    Pago = 500m
                };
                this.Razon = "razon";
            }
        }

        private void DevolucionItemViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(this.Razon):
                    this.AcceptCommand.RaiseCanExecuteChanged();
                    break;
                case nameof(this.SelectedOpcion):
                    if (!((this.SelectedOpcion?.Comentarios) ?? false))
                    {
                        this.Razon = null;
                    }
                    break;
            }
        }
        private IEnumerable<Common.Entities.RazonNotaDevolucion> _Options;
        public IEnumerable<Common.Entities.RazonNotaDevolucion> Options
        {
            get { return _Options; }
            set { Set(nameof(this.Options), ref _Options, value); }
        }
        private Common.Entities.RazonNotaDevolucion _SelectedOpcion;
        public Common.Entities.RazonNotaDevolucion SelectedOpcion
        {
            get { return _SelectedOpcion; }
            set { Set(nameof(this.SelectedOpcion), ref _SelectedOpcion, value); }
        }


        private Common.Entities.ProductoDevolucion _Item;
        public Common.Entities.ProductoDevolucion Item
        {
            get { return _Item; }
            set { Set(nameof(this.Item), ref _Item, value); }
        }
        private string _Razon;
        public string Razon
        {
            get { return _Razon; }
            set { Set(nameof(this.Razon), ref _Razon, value); }
        }

        public string Title => "Devolucion";

        public bool CloseTab => false;

        protected override bool CanAccept()
        {
            return !string.IsNullOrWhiteSpace(this.Razon)
                && this.SelectedOpcion != null;
        }

        protected override void Accept()
        {            
            Messenger.Default.Send(new Messages.ProductoDevolucionMessage
            { 
                Item = new Models.ProductoDevolucion
                {
                    Item = this.Item,
                    Razon = this.Razon, 
                    RazonId = this.SelectedOpcion.Id
                }
            }, this.GID);
        }
    }
}

using GalaSoft.MvvmLight.Messaging;
using SirCoPOS.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Client.ViewModels.Caja
{
    class NotaViewModel : Helpers.ModalViewModelBase, Utilities.Interfaces.IModalItem//<Models.Producto>
    {
        private Common.ServiceContracts.IDataServiceAsync _data;
        public NotaViewModel()
        {
            if (!this.IsInDesignMode)
            {
                _data = CommonServiceLocator.ServiceLocator.Current.GetInstance<Common.ServiceContracts.IDataServiceAsync>();
                this.Options = _data.GetRazonesDevolucion();
            }

            this.RestoreCommand = new GalaSoft.MvvmLight.Command.RelayCommand(() =>
            {
                this.Item.Precio = this.ProductoItem.PrecioOriginal;
                this.ProductoItem.NotaRazon = null;
                this.ProductoItem.Notas = null;
                GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(
                    new Utilities.Messages.ModalResponse { }, this.GID);
            });
            this.PropertyChanged += NotaViewModel_PropertyChanged;
            if (this.IsInDesignMode)
            {
                this.Precio = 1799.89m;
                this.Razon = "se respeta precio de aparador";
                this.Options = new Common.Entities.RazonNotaDevolucion[] {
                    new Common.Entities.RazonNotaDevolucion { Id = 1, Descripcion = "op1", Comentarios = false },
                    new Common.Entities.RazonNotaDevolucion { Id = 2, Descripcion = "op2", Comentarios = false },
                    new Common.Entities.RazonNotaDevolucion { Id = 3, Descripcion = "op3", Comentarios = false },
                };
                this.SelectedOpcion = this.Options.First();
            }
        }

        private void NotaViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            this.AcceptCommand.RaiseCanExecuteChanged();
        }

        public string Title => "Nota";
        public bool CloseTab => false;
        protected override bool CanAccept()
        {
            return (this.Precio ?? 0) > 0
                && !string.IsNullOrWhiteSpace(this.Razon)
                && this.SelectedOpcion != null;
        }

        protected override void Accept()
        {
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(
                new Messages.NotaItem {
                    Precio = this.Precio, 
                    Razon = this.Razon,
                    TipoRazon = this.SelectedOpcion.Id
                }, this.GID);
        }

        private decimal? _Precio;
        public decimal? Precio
        {
            get { return _Precio; }
            set { Set(nameof(this.Precio), ref _Precio, value); }
        }

        private string _Razon;
        public string Razon
        {
            get { return _Razon; }
            set { Set(nameof(this.Razon), ref _Razon, value); }
        }

        private Utilities.Interfaces.IProducto _Item;
        public Utilities.Interfaces.IProducto Item
        {
            get { return _Item; }
            set { Set(nameof(this.Item), ref _Item, value); }
        }
        public Models.Producto ProductoItem
        {
            get => this.Item as Models.Producto;
        }
        public GalaSoft.MvvmLight.Command.RelayCommand RestoreCommand { get; private set; }
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
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel;
using System.Text;
using Xamarin.Forms;

namespace SirCoPOS.App.ViewModels
{
    class MainViewModel : Helpers.ViewModelBase
    {
        private Contracts.Services.IService _proxy;
        public MainViewModel()
        {
            this.PropertyChanged += MainViewModel_PropertyChanged;
            this.Productos = new ObservableCollection<Models.ProductoItem>();
            this.Productos.CollectionChanged += Productos_CollectionChanged;
            this.AddCommand = new Command(() =>
            {
                var com = new Helpers.CommonHelper();
                var ser = com.PrepareSerie(this.Serie);
                var item = _proxy.ScanProducto(ser, this.Sucursal);
                if (item != null)
                {
                    this.Serie = null;
                    this.Productos.Add(new Models.ProductoItem { 
                        Item = new Models.Producto 
                        {
                            Id = item.Id,
                            Serie = item.Serie,
                            //public string Marca { get; set; }
                            //public string Modelo { get; set; }
                            Precio = item.Precio,
                            HasImage = item.HasImage
                        } 
                    });
                }
                //this.ImageUrl = String.Format($"{Constants.Parameters.ServiceUrl}/Images/Producto?marca={0}&modelo={1}", "CTA", 83);
            }, () => !String.IsNullOrEmpty(this.Serie));

            this.SaleCommand = new Command(() => {

                var request = new SirCoPOS.Contracts.Entities.SaleRequest
                {
                    Sucursal = this.Sucursal, 
                    Pagar = this.Pagar, 
                    Series = this.Productos.Select(i => i.Item.Serie)
                };
                var res = _proxy.Sale(request);
                if (res != null)
                {
                    MessagingCenter.Send(new Messages.Alert { Message = $"Folio: {res.Folio}" }, "");
                }
                else
                {
                    MessagingCenter.Send(new Messages.Alert { Message = "error" }, "");
                }

                this.Productos.Clear();
                this.Pagar = null;
            }, () => (this.Total ?? 0) > 0 && this.Pagar == this.Total);

            if (this.IsInDesignMode)
            {
                this.Productos.Add(new Models.ProductoItem { Item = new Models.Producto { Serie = "123" } });
                this.Productos.Add(new Models.ProductoItem { Item = new Models.Producto { Serie = "456" } });
                this.Productos.Add(new Models.ProductoItem { Item = new Models.Producto { Serie = "789" } });
            }

            if (!this.IsInDesignMode)
            {
                var myBinding = new BasicHttpBinding();
                var myEndpoint = new EndpointAddress($"{Constants.Parameters.ServiceUrl}/Service.svc");
                _proxy = new System.ServiceModel.ChannelFactory<Contracts.Services.IService>(myBinding, myEndpoint).CreateChannel();
            }
        }

        private void Productos_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            this.RaisePropertyChanged(nameof(this.Total));
            this.SaleCommand.ChangeCanExecute();
        }

        private void MainViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(this.Serie):
                    this.AddCommand.ChangeCanExecute();
                    break;
                case nameof(this.Pagar):
                    this.SaleCommand.ChangeCanExecute();
                    break;
            }
        }

        #region properties
        private string _imageUrl;
        public string ImageUrl
        {
            get => _imageUrl;
            set
            {
                _imageUrl = value;
                this.RaisePropertyChanged(nameof(this.ImageUrl));
            }
        }
        public ObservableCollection<Models.ProductoItem> Productos { get; private set; }
        private string _sucursal;
        public string Sucursal
        {
            get => _sucursal;
            set
            {
                _sucursal = value;
                this.RaisePropertyChanged(nameof(this.Sucursal));
            }
        }
        private string _serie;
        public string Serie
        {
            get => _serie;
            set
            {
                _serie = value;
                this.RaisePropertyChanged(nameof(this.Serie));
            }
        }
        private decimal? _pagar;
        public decimal? Pagar
        {
            get => _pagar;
            set
            {
                _pagar = value;
                this.RaisePropertyChanged(nameof(this.Pagar));
            }
        }
        #endregion
        #region computed
        public decimal? Total
        {
            get {
                if (this.Productos.Count > 0)
                {
                    var res = this.Productos.Sum(i => i.Item.Precio);
                    return res;
                }
                return null;
            }
        }
        #endregion
        #region commands
        public Command AddCommand { get; private set; }
        public Command SaleCommand { get; private set; }
        #endregion
    }
}

using SirCoPOS.Common.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SirCoPOS.Client.ViewModels.Tabs
{
    class CancelacionViewModel : Helpers.TabViewModelBase
    {
        private Common.ServiceContracts.IDataServiceAsync _proxy;
        private Helpers.CommonHelper _common;
        private Helpers.ServiceClient _client;
        public CancelacionViewModel()
        {
            if (!IsInDesignMode)
            {
                _proxy = CommonServiceLocator.ServiceLocator.Current.GetInstance<Common.ServiceContracts.IDataServiceAsync>();
                _client = new Helpers.ServiceClient();
            }            
            _common = new Helpers.CommonHelper();
            this.PropertyChanged += CancelacionViewModel_PropertyChanged;
            this.Productos = new ObservableCollection<Models.CancelProducto>();
            this.Productos.CollectionChanged += Productos_CollectionChanged;

            this.ScanCommand = new GalaSoft.MvvmLight.Command.RelayCommand(async () => {
                this.IsBusy = true;
                await Scan();
                this.IsBusy = false;
            }, () => !string.IsNullOrWhiteSpace(this.SerieSearch));

            this.CancelCommand = new GalaSoft.MvvmLight.Command.RelayCommand(async () => {
                this.IsBusy = true;

                var request = new Common.Entities.CancelSaleRequest
                {
                    Sucursal = this.Venta.Sucursal,
                    Folio = this.Venta.Folio
                };
                await _client.CancelSaleAsync(request);

                this.Complete();

                this.IsBusy = false;
            }, () => this.Productos.Any() && !this.Productos.Where(i => !i.Scanned).Any());

            if (this.IsInDesignMode)
            {
                this.ErrorMessage = "error";
                this.Venta = new Models.SucursalFolio { Sucursal = "01", Folio = "414628" };
                this.SerieSearch = "0000003343805";
                this.Productos.Add(new Models.CancelProducto { Scanned = true, Producto = new Producto { Id = 1, Serie = "001", Marca = "a", Modelo = "b", Talla = "c", Precio = 100, Total = 100, HasImage = true } });
                this.Productos.Add(new Models.CancelProducto { Scanned = true, Producto = new Producto { Id = 2, Serie = "002", Marca = "a", Modelo = "b", Talla = "c", Precio = 100, Total = 1000, HasImage = true } });
                this.Productos.Add(new Models.CancelProducto { Scanned = false, Producto = new Producto { Id = 3, Serie = "003", Marca = "a", Modelo = "b", Talla = "c", Precio = 100, Total = 1234.25m } });
                this.Productos.Add(new Models.CancelProducto { Scanned = true, Producto = new Producto { Id = 4, Serie = "004", Marca = "a", Modelo = "b", Talla = "c", Precio = 100, Total = 12456 } });
                this.Productos.Add(new Models.CancelProducto { Scanned = false, Producto = new Producto { Id = 5, Serie = "005", Marca = "a", Modelo = "b", Talla = "c", Precio = 100, Total = 9.99m } });
            }
        }
        private async Task Scan()
        {
            var ser = _common.PrepareSerie(this.SerieSearch);

            if (this.Productos.Any())
            {
                var item = this.Productos.Where(i => i.Producto.Serie == ser).SingleOrDefault();
                if (item != null)
                {
                    this.SerieSearch = null;
                    item.Scanned = true;
                }
                else
                    this.ErrorMessage = Resources.Resource.Cancelacion_NoEncontrado;
            }
            else
            {
                var item = await _proxy.ScanProductoDevolucionAsync(ser, cancelacion: true);
                if (item != null && item.Success)
                {
                    this.SerieSearch = null;
                    var res = await _proxy.FindSaleAsync(item.Producto.Sucursal, item.Producto.Folio);
                    if (res != null)
                    {
                        if (this.Sucursal.Clave != res.Sucursal)
                        {
                            MessageBox.Show($"La venta pertenece a otra sucursal ({res.Sucursal})");
                            return;
                        }

                        this.Venta = new Models.SucursalFolio
                        {
                            Folio = res.Folio,
                            Sucursal = res.Sucursal
                        };
                        foreach (var p in res.Productos)
                        {
                            var can = new Models.CancelProducto { Producto = p };
                            if (can.Producto.Serie == ser)
                                can.Scanned = true;
                            can.PropertyChanged += (s, e) => { this.CancelCommand.RaiseCanExecuteChanged(); };
                            this.Productos.Add(can);
                        }
                    }
                }
                else
                {
                    if(item == null)
                        this.ErrorMessage = "Articulo no encontrado";
                    else
                        this.ErrorMessage = "Articulo no valido";
                    //this.ErrorMessage = Resources.Resource.Cancelacion_NoValido;
                }
            }
        }
        private void CancelacionViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(this.SubTotal):
                case nameof(this.Descuento):
                    this.RaisePropertyChanged(nameof(this.Total));
                    break;
                case nameof(this.SerieSearch):
                    this.ErrorMessage = null;
                    break;
            }
        }

        private void Productos_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            RaisePropertyChanged(nameof(this.SubTotal));
            RaisePropertyChanged(nameof(this.Descuento));
            this.CancelCommand.RaiseCanExecuteChanged();
        }
        #region commands
        public GalaSoft.MvvmLight.Command.RelayCommand CancelCommand { get; private set; }
        public GalaSoft.MvvmLight.Command.RelayCommand ScanCommand { get; private set; }
        #endregion
        #region computed
        public decimal SubTotal
        {
            get { return this.Productos.Sum(i => i.Producto.Precio.Value); }
        }
        public decimal Descuento
        {
            get { return 0m /*this.Productos.Where(i => i.Producto.Descuento.HasValue).Sum(i => i.Producto.Descuento.Value)*/; }
        }
        public decimal Total
        {
            get { return this.SubTotal - this.Descuento; }
        }
        #endregion
        #region properties
        private string _ErrorMessage;
        public string ErrorMessage
        {
            get { return _ErrorMessage; }
            set { Set(nameof(this.ErrorMessage), ref _ErrorMessage, value); }
        }

        public ObservableCollection<Models.CancelProducto> Productos { get; set; }
        private string _serieSearch;
        public string SerieSearch
        {
            get { return _serieSearch; }
            set { Set(nameof(this.SerieSearch), ref _serieSearch, value); }
        }        
        private Models.SucursalFolio _venta;
        public Models.SucursalFolio Venta
        {
            get { return _venta; }
            set { Set(nameof(this.Venta), ref _venta, value); }
        }
        private string _folio;
        public string Folio
        {
            get { return _folio; }
            set { Set(nameof(this.Folio), ref _folio, value); }
        }
        #endregion        
    }
}

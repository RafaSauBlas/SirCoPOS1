using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using GalaSoft.MvvmLight.Command;
using SirCoPOS.Common.Entities;

namespace SirCoPOS.Client.ViewModels.Tabs
{
    public class CancelacionDevolucionViewModel : Helpers.TabViewModelBase
    {
        private Common.ServiceContracts.IDataServiceAsync _data;
        private Common.ServiceContracts.IDataServiceAsync _proxy;
        private Helpers.CommonHelper _common;
        private Helpers.ServiceClient _client;
        public CancelacionDevolucionViewModel()
        {
            if (!this.IsInDesignMode)
            {
                _data = CommonServiceLocator.ServiceLocator.Current.GetInstance<Common.ServiceContracts.IDataServiceAsync>();
                _proxy = CommonServiceLocator.ServiceLocator.Current.GetInstance<Common.ServiceContracts.IDataServiceAsync>();
                _client = new Helpers.ServiceClient();
            }
            _common = new Helpers.CommonHelper();
            this.Productos = new ObservableCollection<Models.CancelProducto>();
            this.CancelCommand = new GalaSoft.MvvmLight.Command.RelayCommand(async () => {
                this.IsBusy = true;

                var request = new Common.Entities.CancelSaleRequest
                {
                    Sucursal = this.Devolucion.Sucursal,
                    Folio = this.Devolucion.Folio
                };
                await _client.CancelReturnAsync(request.Sucursal, request.Folio);

                this.Complete();
                MessageBox.Show("Cancelación realizada exitosamente", "Cancelación Devolución", MessageBoxButton.OK, MessageBoxImage.Information);
                this.CloseCommand.Execute(null);
                this.IsBusy = false;
            }, () => this.Productos.Any() && !this.Productos.Where(i => !i.Scanned).Any());
            this.SearchCommand = new RelayCommand(() =>
            {
                var ser = _common.PrepareSerie(this.Search);
                var item = _data.ScanProductoFromDevolucion(ser);
                if (!this.Productos.Any())
                {
                    if (item != null && item.Success)
                    {
                        this.Search = null;
                        var dev = _data.FindDevolucionView(item.Producto.Sucursal, item.Producto.Folio, this.Cajero.Id);

                        if (this.Sucursal.Clave != dev.Sucursal)
                        {
                            MessageBox.Show($"La devolución pertenece a otra sucursal ({dev.Sucursal})");
                            return;
                        }

                        this.Devolucion = new Models.SucursalFolio
                        {
                            Sucursal = dev.Sucursal, 
                            Folio = dev.Folio
                        };
                        foreach (var ditem in dev.Productos)
                        {
                            var can = new Models.CancelProducto
                            {
                                Producto = new Producto
                                {
                                    Id = ditem.ArticuloId, 
                                    Modelo = ditem.Modelo, 
                                    Marca = ditem.Marca, 
                                    Precio = ditem.Precio, 
                                    Serie = ditem.Serie, 
                                    Talla = ditem.Medida
                                }
                            };
                            this.Productos.Add(can);
                            can.PropertyChanged += (s, e) => { this.CancelCommand.RaiseCanExecuteChanged(); };
                            if (can.Producto.Serie == ser)
                                can.Scanned = true;                            
                        }
                    }
                    else
                    {
                        this.ErrorMessage = "Artículo no encontrado";
                    }
                }
                else
                {
                    var pitem = this.Productos.Where(i => i.Producto.Serie == ser).SingleOrDefault();
                    if (pitem != null)
                    {
                        this.Search = null;
                        pitem.Scanned = true;
                    }
                    else
                        this.ErrorMessage = Resources.Resource.Cancelacion_NoEncontrado;
                }     
            }, () => !string.IsNullOrEmpty(this.Search));

            this.NadaCommand = new RelayCommand(() => {

            });

            if (this.IsInDesignMode)
            {
                this.Search = "0000003678429";
                this.Devolucion = new Models.SucursalFolio
                {
                    Sucursal = "00",
                    Folio = "000123"
                };
                this.Productos.Add(new Models.CancelProducto { Scanned = true, Producto = new Producto { Id = 1, Serie = "001", Marca = "a", Modelo = "b", Talla = "c", Precio = 100, Total = 100, HasImage = true } });
                this.Productos.Add(new Models.CancelProducto { Scanned = true, Producto = new Producto { Id = 2, Serie = "002", Marca = "a", Modelo = "b", Talla = "c", Precio = 100, Total = 1000, HasImage = true } });
                this.Productos.Add(new Models.CancelProducto { Scanned = false, Producto = new Producto { Id = 3, Serie = "003", Marca = "a", Modelo = "b", Talla = "c", Precio = 100, Total = 1234.25m } });
                this.Productos.Add(new Models.CancelProducto { Scanned = true, Producto = new Producto { Id = 4, Serie = "004", Marca = "a", Modelo = "b", Talla = "c", Precio = 100, Total = 12456 } });
                this.Productos.Add(new Models.CancelProducto { Scanned = false, Producto = new Producto { Id = 5, Serie = "005", Marca = "a", Modelo = "b", Talla = "c", Precio = 100, Total = 9.99m } });
            }
        }
        private async Task Scan()
        {
            var ser = _common.PrepareSerie(this.Search);

            if (this.Productos.Any())
            {
                var item = this.Productos.Where(i => i.Producto.Serie == ser).SingleOrDefault();
                if (item != null)
                {
                    this.Search = null;
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
                    this.Search = null;
                    var res = await _proxy.FindSaleAsync(item.Producto.Sucursal, item.Producto.Folio);
                    if (res != null)
                    {
                        this.Devolucion = new Models.SucursalFolio
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
                    if (item == null)
                        this.ErrorMessage = "Artículo no encontrado";
                    else
                        this.ErrorMessage = "Artículo no valido";
                    //this.ErrorMessage = Resources.Resource.Cancelacion_NoValido;
                }
            }
        }
        #region commands
        public RelayCommand CancelCommand { get; private set; }
        public RelayCommand SearchCommand { get; private set; }
        public RelayCommand NadaCommand { get; private set; }
        #endregion
        private string _ErrorMessage;
        public string ErrorMessage
        {
            get { return _ErrorMessage; }
            set { Set(nameof(this.ErrorMessage), ref _ErrorMessage, value); }
        }
        private string _search;
        public string Search
        {
            get => _search;
            set
            {
                if (Helpers.ScanSerie.PorScanner(value, Cajero.Depto))
                {
                    Set(nameof(this.Search), ref _search, value);
                }
                else
                {
                    Set(nameof(this.Search), ref _search, "");
                }
            }

        }
        private Models.SucursalFolio _devolucion;
        public Models.SucursalFolio Devolucion
        {
            get => _devolucion;
            set => this.Set(nameof(Devolucion), ref _devolucion, value);
        }        
        public ObservableCollection<Models.CancelProducto> Productos { get; set; }
    }
}

using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
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
    class DevolucionViewModel : Helpers.TabViewModelBase
    {
        private readonly Common.ServiceContracts.IDataServiceAsync _proxy;
        private readonly Helpers.CommonHelper _common;
        private readonly Helpers.ServiceClient _client;
        private Helpers.ReportsHelper _reports;
        public DevolucionViewModel()
        {
            _common = new Helpers.CommonHelper();
            this.PropertyChanged += DevolucionViewModel_PropertyChanged;
            this.Productos = new ObservableCollection<Models.ProductoDevolucion>();
            this.Productos.CollectionChanged += Productos_CollectionChanged;
            if (!IsInDesignMode)
            {
                _reports = new Helpers.ReportsHelper();
                _client = new Helpers.ServiceClient();
                _proxy = CommonServiceLocator.ServiceLocator.Current.GetInstance<Common.ServiceContracts.IDataServiceAsync>();
            }
            this.LoadCommand = new GalaSoft.MvvmLight.Command.RelayCommand(async () => {
                var ser = _common.PrepareSerie(this.SerieSearch);

                var item = await _proxy.ScanProductoDevolucionAsync(ser, cancelacion: false);
                if (item != null && item.Success)
                {
                    if (!this.Productos.Any())
                    {
                        AddItem(item.Producto);
                        this.SerieSearch = null;
                        this.Venta = new Models.SucursalFolio { Sucursal = item.Producto.Sucursal, Folio = item.Producto.Folio };

                        //var ven = _proxy.FindVentaView(this.Venta.Sucursal, this.Venta.Folio, 0);     

                        var sale = _proxy.FindSale(this.Venta.Sucursal, this.Venta.Folio);
                        this.CanSetClient = !sale.ClienteId.HasValue;
                        if (sale.ClienteId.HasValue)
                        {
                            this.Cliente = _proxy.FindCliente(sale.ClienteId.Value);
                            if (this.Cliente != null)
                            {
                                this.NuevoCliente = new Models.NuevoCliente
                                {
                                    Id = this.Cliente.Id,
                                    Nombre = this.Cliente.Nombre,
                                    ApPaterno = this.Cliente.ApPaterno,
                                    ApMaterno = this.Cliente.ApMaterno
                                };
                            }
                        }

                    }
                    else
                    {
                        if (this.Venta.Sucursal == item.Producto.Sucursal
                            && this.Venta.Folio == item.Producto.Folio)
                        {
                            var current = this.Productos.Where(i => i.Item.Serie == item.Producto.Serie).SingleOrDefault();
                            if (current == null)
                                AddItem(item.Producto);
                            else
                                this.Productos.Remove(current);
                            this.SerieSearch = null;
                        }
                        else
                            MessageBox.Show($"El producto {item.Producto.Serie}, no pertenece a la misma venta.");
                    }
                }
                else
                {
                    if (item == null)
                        this.ErrorMessage = "Artículo no encontrado";
                    else
                    {
                        if(item.Status == Common.Constants.Status.BA)
                            this.ErrorMessage = "El articulo ha excedido el tiempo valido para su devolución o cambio";
                        else
                            this.ErrorMessage = "Artículo no valido";
                    }
                }
            }, () => !string.IsNullOrWhiteSpace(this.SerieSearch));
            this.ReturnCommand = new GalaSoft.MvvmLight.Command.RelayCommand(async () => {

                //Messenger.Default.Send(new Messages.RequestApproval { GID = this.GID });

                this.IsBusy = true;
                var request = new Common.Entities.ReturnRequest
                {
                    Sucursal = this.Venta.Sucursal,
                    Folio = this.Venta.Folio,
                    Comments = "",
                    Items = this.Productos.Select(i => i.Item.Serie),
                    Razones = this.Productos.ToDictionary(i => i.Item.Serie, 
                        i => new Common.Entities.RazonItem { 
                            TipoRazon = i.RazonId.Value, 
                            Notas = i.Razon 
                        })
                };
                request.Cliente = Helpers.Parsers.PaseCliente(this.NuevoCliente, this.Cliente, this.Sucursal);
                this.Folio = await _client.ReturnAsync(request);                
                this.IsBusy = false;

                _reports.Devolucion(this.Sucursal.Clave, this.Folio);
                this.CloseCommand.Execute(null);
            }, () => this.Total > 0);
            
            this.PrintCommand = new RelayCommand(() => {

                _reports.Devolucion(this.Sucursal.Clave, this.Folio);

                this.CloseCommand.Execute(null);

            }, () => this.IsComplete);

            this.LoadClienteCommand = new RelayCommand(() =>
            {
                Messenger.Default.Send(
                            new Utilities.Messages.OpenModal
                            {
                                Name = Utilities.Constants.Modals.cliente,
                                GID = this.GID
                            });
            }, () => this.CanSetClient);
            this.NadaCommand = new RelayCommand(() => {

            });
            this.ClearClienteCommand = new RelayCommand(() =>
            {
                this.Cliente = null;
                this.NuevoCliente = null;
            }, () => this.CanSetClient);
            if (this.IsInDesignMode)
            {
                this.ClienteId = 90;
                this.Folio = "123";
                this.SerieSearch = "0000003342601";
                this.Venta = new Models.SucursalFolio { Folio = "folio", Sucursal = "sucursal" };
                this.Productos.Add(new Models.ProductoDevolucion { Item = new ProductoDevolucion { Sucursal = "01", Folio = "123", Serie = "0000003413693", Marca = "FFF", Modelo = "2608", Talla = "27.5", Precio = 799 } });
                this.Productos.Add(new Models.ProductoDevolucion { Item = new ProductoDevolucion { Sucursal = "01", Folio = "124", Serie = "0000003420542", Marca = "AAA", Modelo = "1234", Talla = "28", Precio = 123.45m } });

                this.NuevoCliente = new Client.Models.NuevoCliente { Nombre = "nom", ApPaterno = "ap pa", ApMaterno = "ap ma" };
            }
        }
        protected override void RegisterMessages()
        {
            Messenger.Default.Register<Utilities.Messages.ApprovedResponse>(this, this.GID, async p => {

                this.IsBusy = true;
                var request = new Common.Entities.ReturnRequest
                {
                    Sucursal = this.Venta.Sucursal,
                    Folio = this.Venta.Folio,
                    Comments = "",
                    Items = this.Productos.Select(i => i.Item.Serie),
                    Razones = this.Productos.ToDictionary(i => i.Item.Serie,
                        i => new Common.Entities.RazonItem
                        {
                            TipoRazon = i.RazonId.Value,
                            Notas = i.Razon
                        })
                };
                request.Cliente = Helpers.Parsers.PaseCliente(this.NuevoCliente, this.Cliente, this.Sucursal);
                this.Folio = await _client.ReturnAsync(request);
                this.IsBusy = false;

            });

            Messenger.Default.Register<Messages.ProductoDevolucionMessage>(this, this.GID, p => {
                this.Productos.Add(p.Item);
            });
            Messenger.Default.Register<Messages.ClienteMessage>(this, this.GID, m => {
                this.Cliente = m.Cliente;
                this.NuevoCliente = null;
                if (this.Cliente != null)
                {
                    this.NuevoCliente = new Models.NuevoCliente
                    {
                        Id = this.Cliente.Id,
                        Nombre = this.Cliente.Nombre,
                        ApPaterno = this.Cliente.ApPaterno,
                        ApMaterno = this.Cliente.ApMaterno
                    };
                }
            });
            Messenger.Default.Register<Messages.NuevoClienteMessage>(this, this.GID, m => {
                this.NuevoCliente = m.Cliente;
                this.Cliente = null;
            });
        }
        private void AddItem(ProductoDevolucion producto)
        {
            Messenger.Default.Send(
                               new Utilities.Messages.OpenModalDevolucionItem
                               {
                                   GID = this.GID,
                                   Item = producto
                               });
        }

        private void DevolucionViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(this.CanSetClient):
                    this.ClearClienteCommand.RaiseCanExecuteChanged();
                    this.LoadClienteCommand.RaiseCanExecuteChanged();
                    break;
                case nameof(this.SerieSearch):
                    this.ErrorMessage = null;
                    break;
                case nameof(this.Folio):
                    this.RaisePropertyChanged(nameof(this.IsComplete));
                    break;
                case nameof(this.IsComplete):
                    this.PrintCommand.RaiseCanExecuteChanged();
                    break;
            }
        }

        private void Productos_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            this.RaisePropertyChanged(nameof(this.Total));
            this.ReturnCommand.RaiseCanExecuteChanged();
        }
        protected override bool IsReady()
        {
           return this.Folio != null;
        }
        #region computed        
        #endregion
        #region properties
        private bool _CanSetClient;
        public bool CanSetClient
        {
            get { return _CanSetClient; }
            set { Set(nameof(this.CanSetClient), ref _CanSetClient, value); }
        }

        private int? _clienteId;
        public int? ClienteId
        {
            get => _clienteId;
            set => this.Set(nameof(this.ClienteId), ref _clienteId, value);
        }
        private string _folio;
        public string Folio
        {
            get => this._folio;
            set => this.Set(nameof(this.Folio), ref _folio, value);
        }
        public ObservableCollection<Models.ProductoDevolucion> Productos { get; set; }
        private string _serieSearch;
        public string SerieSearch
        {
            get { return _serieSearch; }
            set
            {
                if (Helpers.ScanSerie.PorScanner(value, Cajero.Depto))
                {
                    Set(nameof(this.SerieSearch), ref _serieSearch, value);
                }
                else
                {
                    Set(nameof(this.SerieSearch), ref _serieSearch, "");
                }
            }

        }
        public decimal Total
        {
            get { return this.Productos.Sum(i => i.Item.Pago ?? 0); }
        }
        private Models.SucursalFolio _venta;
        public Models.SucursalFolio Venta
        {
            get { return _venta; }
            set { this.Set(nameof(this.Venta), ref _venta, value); }
        }
        private string _ErrorMessage;
        public string ErrorMessage
        {
            get { return _ErrorMessage; }
            set { Set(nameof(this.ErrorMessage), ref _ErrorMessage, value); }
        }
        private Models.ProductoDevolucion _SelectedItem;
        public Models.ProductoDevolucion SelectedItem
        {
            get { return _SelectedItem; }
            set { Set(nameof(this.SelectedItem), ref _SelectedItem, value); }
        }
        private Common.Entities.Cliente _cliente;
        public Common.Entities.Cliente Cliente
        {
            get => _cliente;
            set => this.Set(nameof(this.Cliente), ref _cliente, value);
        }
        private Models.NuevoCliente _nuevoCliente;
        public Models.NuevoCliente NuevoCliente
        {
            get => _nuevoCliente;
            set => this.Set(nameof(this.NuevoCliente), ref _nuevoCliente, value);
        }
        #endregion
        #region commands
        public RelayCommand LoadCommand { get; private set; }
        public RelayCommand ReturnCommand { get; private set; }
        public RelayCommand ClearClienteCommand { get; private set; }
        public RelayCommand LoadClienteCommand { get; private set; }
        public RelayCommand NadaCommand { get; private set; }
        public RelayCommand PrintCommand { get; private set; }
        #endregion
    }
}

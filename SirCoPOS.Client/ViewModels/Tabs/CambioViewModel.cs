using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SirCoPOS.Common.Entities;
using System.Collections.ObjectModel;
using System.Windows;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using SirCoPOS.Utilities.Interfaces;
using SirCoPOS.Common.Constants;

namespace SirCoPOS.Client.ViewModels.Tabs
{
    public class CambioViewModel : Helpers.TabsPagosViewModel, ICajaBase
    {
        private Common.ServiceContracts.IDataServiceAsync _proxy;
        private Helpers.CommonHelper _common;
        private Common.Entities.DevolucionResponse _sale;
        private Helpers.ServiceClient _client;
        private AutoMapper.IMapper _mapper;
        private Helpers.ReportsHelper _reports;
        public CambioViewModel()
        {
            this.PropertyChanged += CambioViewModel_PropertyChanged;
            _common = new Helpers.CommonHelper();
            this.Productos = new ObservableCollection<Models.ProductoCambio>();
            this.Productos.CollectionChanged += Productos_CollectionChanged;
            this.Pagos.CollectionChanged += Pagos_CollectionChanged;
            if (!this.IsInDesignMode)
            {
                _reports = new Helpers.ReportsHelper();
                _client = new Helpers.ServiceClient();
                _proxy = CommonServiceLocator.ServiceLocator.Current.GetInstance<Common.ServiceContracts.IDataServiceAsync>();
                _mapper = CommonServiceLocator.ServiceLocator.Current.GetInstance<AutoMapper.IMapper>();
            }

            //this.PagarCommand = new RelayCommand(() => {

            //    this.ShowPagos = true;

            //}, () => this.Total > 0 && this.Remaining > 0);
            //this.AddFormaCommand = new RelayCommand<FormaPago>(fp => {
            //    this.ShowPagos = false;
            //    Messenger.Default.Send(
            //        new Messages.OpenPago
            //        {
            //            GID = this.GID,
            //            FormaPago = fp,
            //            Total = this.Remaining,
            //            Caja = this,
            //            ClientId = this.Cliente?.Id,
            //            ProductosPlazos = this.Productos.Where(i => i.MaxPlazos.HasValue && i.MaxPlazos > 0)
            //        });
            //}, p => {
            //    var q = this._formas.Where(i => i.Key == p);
            //    return q.Any() && this.Total > 0 && this.Remaining > 0 && q.Single().Value.Enabled;
            //});
            this.AddPagoCommand = new RelayCommand(() => {

                this.ShowPagos = true;

            }, () => this.Remaining > 0);

            this.ScanCommand = new GalaSoft.MvvmLight.Command.RelayCommand(async () => {
                this.IsBusy = true;
                var ser = _common.PrepareSerie(this.SerieSearch);

                await this.Scan(ser);
                this.AddPagoCommand.RaiseCanExecuteChanged();
                this.SaveCommand.RaiseCanExecuteChanged();

                this.IsBusy = false;
            }, () => !string.IsNullOrEmpty(this.SerieSearch));

            this.SaveCommand = new GalaSoft.MvvmLight.Command.RelayCommand(async () => {
                this.IsBusy = true;
                var request = new Common.Entities.ChangeRequest
                {
                    Sucursal = this.Venta.Sucursal,
                    Folio = this.Venta.Folio,
                    Items = this.Productos.Select(i => new Common.Entities.ChangeItem
                    {
                        OldItem = i.OldItem.Serie,
                        NewItem = i.NewItem.Serie
                    }),
                    Pagos = this.PreparePagos(),
                    Razones = this.Productos.ToDictionary(i => i.OldItem.Serie,
                        i => new Common.Entities.RazonItem
                        {
                            TipoRazon = i.RazonId.Value,
                            Notas = i.Razon
                        })
                };
                request.Cliente = Helpers.Parsers.PaseCliente(this.NuevoCliente, this.Cliente, this.Sucursal);
                this.Result = await _client.ChangeAsync(request);
                this.IsBusy = false;
            }, () => this.Productos.Any() 
                && !this.Productos.Where(i => !i.Complete).Any()
                && this.Remaining <= 0);

            this.RemovePagoCommand = new RelayCommand(() => {
                //if (!_formas[this.SelectedPago.FormaPago].Duplicate)
                //{
                //    _formas[this.SelectedPago.FormaPago].Enabled = true;
                //    this.FormasPago.Refresh();
                //}
                //_ls.RemovePago(this.SelectedPago.Id);
                this.Pagos.Remove(this.SelectedPago);                
            }, () => this.SelectedPago != null);

            this.LoadClienteCommand = new RelayCommand(() => {
                Messenger.Default.Send(
                            new Utilities.Messages.OpenModal
                            {
                                Name = Utilities.Constants.Modals.cliente,
                                GID = this.GID
                            });
            });
            this.ClearClienteCommand = new RelayCommand(() => {
                this.Cliente = null;
                this.NuevoCliente = null;
                foreach (var item in this.Pagos.Where(i => i.ClientId.HasValue).ToArray())
                {
                    this.Pagos.Remove(item);
                    //_ls.RemovePago(item.Id);
                }
                this.ClientConfirmed = false;
                this.FormasPago.Refresh();                
            });

            this.PrintCommand = new RelayCommand<string>(rpt => {
                if(rpt == "venta")
                    _reports.Compra(this.Sucursal.Clave, this.Result.Venta);
                if (rpt == "devolucion")
                    _reports.Devolucion(this.Sucursal.Clave, this.Result.Devolucion);
            }, rpt => this.IsComplete);

            if (this.IsInDesignMode)
            {
                this.Result = new ChangeResponse {
                    Devolucion = "111",
                    Venta = "222",
                    Cliente = 333
                };
                this.SerieSearch = "0000003342601";
                this.Venta = new Models.SucursalFolio { Sucursal = "01", Folio = "000123" };
                this.Productos.Add(new Models.ProductoCambio {
                    OldItem = new ProductoDevolucion { Sucursal = "01", Folio = "123", Serie = "0000003413693", Marca = "FFF", Modelo = "2608", Talla = "27.5", Precio = 799 },
                    NewItem = new Models.Producto { Serie = "0000003420542", Marca = "AAA", Modelo = "1234", Talla = "28", Precio = 123.45m }
                });
                this.Productos.Add(new Models.ProductoCambio
                {
                    OldItem = null,
                    NewItem = new Models.Producto { Serie = "0000003420542", Marca = "AAA", Modelo = "1234", Talla = "28", Precio = 123.45m }
                });
                this.Productos.Add(new Models.ProductoCambio
                {
                    OldItem = new ProductoDevolucion { Sucursal = "01", Folio = "123", Serie = "0000003413693", Marca = "FFF", Modelo = "2608", Talla = "27.5", Precio = 799 },
                    NewItem = null
                });
                this.NuevoCliente = new Models.NuevoCliente { Nombre = "nom", ApPaterno = "ap pa", ApMaterno = "ap ma" };
            }
        }
        protected override void OpenFormaPago(FormaPago fp)
        {
            Messenger.Default.Send(
                    new Utilities.Messages.OpenPago
                    {
                        GID = this.GID,
                        FormaPago = fp,
                        //Total = this.Remaining,
                        Caja = this,
                        ClientId = this.Cliente?.Id,
                        HasPromocionPlazo = this.Productos.Where(i => !i.NewItem.MaxPlazos.HasValue).Any(),
                        ProductosPlazos = this.Productos.Where(i => i.NewItem != null).Select(i => i.NewItem)
                            .Where(i => i.MaxPlazos.HasValue && i.MaxPlazos > 0)
                    });
        }
        private void Pagos_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            this.AddPagoCommand.RaiseCanExecuteChanged();
            this.SaveCommand.RaiseCanExecuteChanged();
        }

        protected override void RegisterMessages()
        {
            base.RegisterMessages();
            //this.SaveCommand.RaiseCanExecuteChanged();

            Messenger.Default.Register<Messages.ProductoDevolucionMessage>(this, this.GID, p => {
                //this.Productos.Add(p.Item);                
                this.Productos.Add(new Models.ProductoCambio { 
                    OldItem = p.Item.Item, 
                    Razon = p.Item.Razon,
                    RazonId = p.Item.RazonId
                });
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
                this.FormasPago.Refresh();                
            });
            Messenger.Default.Register<Messages.NuevoClienteMessage>(this, this.GID, m => {
                this.NuevoCliente = m.Cliente;
                this.Cliente = null;                                
            });
        }
        private async void Clear(bool release)
        {
            //_ls.Clear();
            if (release)
            {
                foreach (var item in this.Productos.Where(i => i.NewItem != null))
                {
                    await _client.ReleaseProductoAsync(item.NewItem.Serie);
                }
            }
        }
        public override void Close()
        {
            this.Clear(true);
        }
        public override void UpdatePagos()
        {
            
        }
        private void Productos_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            e.NewItems?.OfType<Models.ProductoCambio>().ToList().ForEach(i =>
            {
                i.PropertyChanged += I_PropertyChanged;
            });
            e.OldItems?.OfType<Models.ProductoCambio>().ToList().ForEach(i =>
            {
                i.PropertyChanged -= I_PropertyChanged;
            });
            this.RaisePropertyChanged(nameof(this.Total));
        }

        private void I_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(Models.ProductoCambio.Saldo):
                    this.RaisePropertyChanged(nameof(this.Total));
                    break;
            }            
        }

        protected override bool IsReady()
        {
            return this.Result != null;
        }
        private void CambioViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(this.Total):
                    this.RaisePropertyChanged(nameof(this.Devolucion));
                    this.RaisePropertyChanged(nameof(this.Pagar));
                    break;
                case nameof(this.TotalPayment):
                    this.RaisePropertyChanged(nameof(this.Remaining));
                    break;
                case nameof(this.Result):
                    this.RaisePropertyChanged(nameof(IsComplete));
                    break;
            }
        }

        private async Task Scan(string ser)
        {
            if (this.Productos.Any())
            {
                var current = this.Productos.Where(i => i.OldItem != null && i.OldItem.Serie == ser).SingleOrDefault();
                if (current != null)
                {
                    if (current.NewItem == null)
                    {
                        var res = MessageBox.Show($"La serie '{ser}' ya está registrada en el cambio\nDesea removerla?", "Confirmar", MessageBoxButton.YesNo);
                        if (res == MessageBoxResult.Yes)
                            this.Productos.Remove(current);
                    }
                    else
                    {
                        var res = MessageBox.Show($"La serie '{ser}' ya está registrada en el cambio\nDesea removerla?", "Confirmar", MessageBoxButton.YesNo);
                        if (res == MessageBoxResult.Yes)
                            current.OldItem = null;
                    }
                    this.SerieSearch = null;
                    return;
                }

                var curnew = this.Productos.Where(i => i.NewItem != null && i.NewItem.Serie == ser).SingleOrDefault();
                if (curnew != null)
                {
                    this.SerieSearch = null;
                    if (curnew.OldItem == null)
                    {
                        var res = MessageBox.Show($"La serie '{ser}' ya está registrada en el cambio\nDesea removerla?", "Confirmar", MessageBoxButton.YesNo);
                        if (res == MessageBoxResult.Yes)
                        {
                            await _client.ReleaseProductoAsync(curnew.NewItem.Serie);
                            this.Productos.Remove(curnew);
                        }
                    }
                    else
                    {
                        var res = MessageBox.Show($"La serie '{ser}' ya está registrada en el cambio\nDeseas removerla?", "Confirmar", MessageBoxButton.YesNo);
                        if (res == MessageBoxResult.Yes)
                        {
                            await _client.ReleaseProductoAsync(curnew.NewItem.Serie);
                            curnew.NewItem = null;
                        }
                    }
                    return;
                }


                var add = _sale.Productos.Where(i => i.Serie == ser).SingleOrDefault();
                if (add != null)
                {
                    var cur = this.Productos.Where(i => i.OldItem == null
                        && i.NewItem.Corrida == add.Corrida
                        && i.NewItem.Marca == add.Marca
                        && i.NewItem.Modelo == add.Modelo).SingleOrDefault();
                    var old = new Common.Entities.ProductoDevolucion
                    {
                        Id = add.Id.Value,
                        Sucursal = _sale.Sucursal,
                        Folio = _sale.Folio,
                        Serie = add.Serie,
                        Marca = add.Marca,
                        Modelo = add.Modelo,
                        Talla = add.Talla,
                        Corrida = add.Corrida,
                        Precio = add.Precio, 
                        Pago = add.precdesc
                    };
                    if (cur != null)
                        cur.OldItem = old;
                    else
                        this.AddItem(old);

                    this.SerieSearch = null;
                    return;
                }

                var scan = await _proxy.ScanProductoAsync(ser, this.Sucursal.Clave);
                if (scan != null)
                {
                    var svalid = new Common.Constants.Status[] {
                        Common.Constants.Status.AC,
                        Common.Constants.Status.IF,
                        Common.Constants.Status.AB
                    };
                    if (!svalid.Contains(scan.Status)
                        && !(scan.Status == Status.CA && scan.UsuarioCajaId == this.Cajero.Id))
                    {
                        MessageBox.Show($"{scan.Producto.Serie} - {scan.Status}");
                        return;
                    }

                    var cur = this.Productos.Where(i => i.OldItem != null
                        && i.OldItem.Corrida == scan.Producto.Corrida
                        && i.OldItem.Marca == scan.Producto.Marca
                        && i.OldItem.Modelo == scan.Producto.Modelo
                        && i.NewItem == null).FirstOrDefault();
                    if (cur == null)
                    {
                        cur = this.Productos.Where(i => i.OldItem != null
                            && i.OldItem.Marca == scan.Producto.Marca
                            && i.OldItem.Modelo == scan.Producto.Modelo
                            && i.OldItem.Precio == scan.Producto.Precio
                            && i.NewItem == null).FirstOrDefault();
                    }
                    if (cur == null)
                    {
                        cur = this.Productos.Where(i => i.OldItem != null
                            && i.OldItem.Marca == scan.Producto.Marca
                            && i.OldItem.Modelo == scan.Producto.Modelo
                            && i.NewItem == null).FirstOrDefault();
                    }
                    if (cur == null)
                    {                        
                        cur = this.Productos.Where(i => i.OldItem != null
                            && i.OldItem.Precio == i.OldItem.Pago
                            && i.OldItem.Precio == scan.Producto.Precio
                            && i.NewItem == null).FirstOrDefault();                        
                    }

                    if (cur != null)
                    {
                        await _client.RequestProductoAsync(scan.Producto.Serie);
                        cur.NewItem = _mapper.Map<Models.Producto>(scan.Producto);
                        this.SerieSearch = null;
                    }
                    else
                    {
                        var series = this.Productos.Where(i => i.OldItem != null).Select(i => i.OldItem.Serie).ToArray();
                        //var valid = _sale.Productos.Where(i => //i.Corrida == scan.Producto.Corrida && 
                        //    i.Marca == scan.Producto.Marca
                        //    && i.Modelo == scan.Producto.Modelo
                        //    && !series.Contains(i.Serie)).Any();
                        //if (valid)
                        //{
                        //    await _client.RequestProductoAsync(scan.Producto.Serie);
                        //    this.Productos.Add(new Models.ProductoCambio { NewItem = scan.Producto });
                        //    this.SerieSearch = null;
                        //}
                        //else
                        {
                            var empty = this.Productos.Where(i => i.OldItem != null && i.NewItem == null).FirstOrDefault();
                            if (empty != null)
                            {
                                await _client.RequestProductoAsync(scan.Producto.Serie);
                                empty.NewItem = _mapper.Map<Models.Producto>(scan.Producto);
                                this.SerieSearch = null;
                            }
                            else
                                MessageBox.Show($"Producto {ser} no valido");
                        }
                    }
                    return;
                }

                //var current = _sale.Productos.Where(i => i.Serie == ser).SingleOrDefault();
            }
            else
            {
                var item = await _proxy.ScanProductoDevolucionAsync(ser, cancelacion: false);
                if (item != null && item.Success)
                {
                    this.Venta = new Models.SucursalFolio { Sucursal = item.Producto.Sucursal, Folio = item.Producto.Folio };
                    _sale = _proxy.FindSale(item.Producto.Sucursal, item.Producto.Folio);
                    if (_sale.ClienteId != null)
                    {
                        this.Cliente = _proxy.FindCliente(_sale.ClienteId.Value);
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
                    this.AddItem(item.Producto);
                    this.SerieSearch = null;
                }
                else
                {
                    if (item == null)
                        MessageBox.Show("Artículo no encontrado", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    else
                        MessageBox.Show("Artículo no valido", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            //scans siguientes ya no van al server

            //var ditem = await _proxy.ScanProductoDevolucionAsync(ser);
            //if (ditem != null)
            //{
            //    if (!this.Productos.Any())
            //    {
            //        this.Venta = new Models.SucursalFolio { Sucursal = ditem.Producto.Sucursal, Folio = ditem.Producto.Folio };
            //        var res = _proxy.FindSale(ditem.Producto.Sucursal, ditem.Producto.Folio);
            //        this.Productos.Add(new Models.ProductoCambio { OldItem = ditem.Producto });
            //    }
            //    else
            //    {
            //        var current = this.Productos.Where(i => i.NewItem.Serie == ditem.Producto.Serie).SingleOrDefault();
            //        if (current != null)
            //        {
            //            current.OldItem = ditem.Producto;
            //        }
            //        else
            //        {
            //            //checar en venta existente
            //            this.Productos.Add(new Models.ProductoCambio { OldItem = ditem.Producto });
            //        }
            //    }
            //    this.SerieSearch = null;
            //}
            //else {
            //    var sitem = await _proxy.ScanProductoAsync(serie: ser, sucursal: this.Sucursal.Clave);
            //    if (sitem != null)
            //    {
            //        if (!this.Productos.Any())
            //        {
            //            this.Productos.Add(new Models.ProductoCambio { NewItem = sitem.Producto });
            //        }
            //        else
            //        {
            //            var current = this.Productos.Where(i => i.OldItem.Serie == sitem.Producto.Serie).SingleOrDefault();
            //            if (current != null)
            //            {
            //                current.NewItem = sitem.Producto;
            //            }
            //            else
            //            {
            //                //checar si es corrida de la venta existente
            //                this.Productos.Add(new Models.ProductoCambio { NewItem = sitem.Producto });
            //            }
            //        }
            //        this.SerieSearch = null;
            //    }
            //}
        }
        private void AddItem(Common.Entities.ProductoDevolucion item)
        {            
            Messenger.Default.Send(
                               new Utilities.Messages.OpenModalDevolucionItem
                               {
                                   GID = this.GID,
                                   Item = item
                               });
        }
        public RelayCommand AddPagoCommand { get; private set; }
        public ObservableCollection<Models.ProductoCambio> Productos { get; set; }
        #region properties
        private string _serieSearch;
        public string SerieSearch
        {
            get { return _serieSearch; }
            set { this.Set(nameof(this.SerieSearch), ref _serieSearch, value); }
        }
        private Models.SucursalFolio _venta;
        public Models.SucursalFolio Venta
        {
            get { return _venta; }
            set { this.Set(nameof(this.Venta), ref _venta, value); }
        }
        private ChangeResponse _result;
        public ChangeResponse Result
        {
            get => this._result;
            set => this.Set(nameof(this.Result), ref _result, value);
        }
        private Models.ProductoCambio _selectedItem;
        public Models.ProductoCambio SelectedItem
        {
            get => _selectedItem;
            set => this.Set(nameof(this.SelectedItem), ref _selectedItem, value);
        }
        //public decimal SubTotalElectronica
        //{
        //    get { return this.Productos.Where(i => i.NewItem != null && i.NewItem.Electronica).Sum(i => i.NewItem.Precio.Value); }
        //}
        //public decimal SubTotalCalzado
        //{
        //    get { return this.Productos.Where(i => i.NewItem != null && !i.NewItem.Electronica).Sum(i => i.NewItem.Precio.Value); }
        //}
        //public decimal DescuentoElectronica { get => 0; }
        //public decimal DescuentoCalzado { get => 0; }
        //public decimal TotalElectronica
        //{
        //    get { return this.SubTotalElectronica - this.DescuentoElectronica; }
        //}
        //public decimal TotalCalzado
        //{
        //    get { return this.SubTotalCalzado - this.DescuentoCalzado; }
        //}
        //public override decimal RemainingElectronica
        //{
        //    get
        //    {
        //        //return this.TotalElectronica - (this.Productos.Where(i => i.Electronica).SelectMany(i => i.FormasPago).Sum(i => i.Importe) ?? 0);
        //        return 0;
        //    }
        //}
        //public override decimal RemainingCalzado
        //{
        //    get
        //    {
        //        //return this.TotalCalzado - (this.Productos.Where(i => !i.Electronica).SelectMany(i => i.FormasPago).Sum(i => i.Importe) ?? 0);
        //        return this.Remaining;
        //    }
        //}
        #endregion
        #region computed
        public override decimal Total {
            get {
                var total = this.Productos.Sum(i => i.Saldo);
                var res = total ?? 0;
                return res;
            }            
        }
        public decimal? Devolucion {
            get {
                var total = this.Total;
                if (total < 0)
                    return total;
                return null;
            }
        }        
        public decimal? Pagar
        {
            get {
                var total = this.Total;
                if (total > 0)
                    return total;
                return null;
            }
        }
        #endregion
        #region commands
        public RelayCommand ScanCommand { get; private set; }
        public RelayCommand SaveCommand { get; private set; }
        public RelayCommand RemovePagoCommand { get; private set; }
        public RelayCommand LoadClienteCommand { get; private set; }
        public RelayCommand LoadVendedorCommand { get; private set; }
        public RelayCommand ClearClienteCommand { get; private set; }
        public RelayCommand<string> PrintCommand { get; private set; }
        #endregion
    }
}

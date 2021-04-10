using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using SirCoPOS.Common.Constants;
using SirCoPOS.Common.Entities;
using SirCoPOS.Utilities.DataAccess.DataObjects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using h = SirCoPOS.Client.Helpers;

namespace SirCoPOS.Client.ViewModels.Tabs
{
    public partial class CajaViewModel : Helpers.TabsPagosViewModel, IDisposable, Utilities.Interfaces.ICaja, IValidatableObject
    {        
        private readonly Common.ServiceContracts.IDataServiceAsync _proxy;
        private readonly Common.ServiceContracts.ICommonServiceAsync _pproxy;
        private Helpers.ServiceClient _client;
        private Helpers.CommonHelper _common;        
        private Utilities.Interfaces.ILocalStorage _ls;
        private AutoMapper.IMapper _mapper;
        private bool _skipPromociones;
        private Helpers.ReportsHelper _reports;
        private Utilities.Interfaces.IScanner _scanner;

        protected virtual Common.Entities.SaleType Tipo => SaleType.Sale;

        public CajaViewModel()
        {            
            _skipPromociones = false;
            _common = new Helpers.CommonHelper();
            this.Productos = new ObservableCollection<Models.Producto>();
            this.Productos.CollectionChanged += Productos_CollectionChanged;            
            this.Cupones = new ObservableCollection<Cupon>();
            this.PromocionesCupones = new ObservableCollection<Promocion>();
            this.PromocionesCupones.CollectionChanged += PromocionesCupones_CollectionChanged;
            _promocionesCuponesUsadas = new CollectionViewSource { Source = this.PromocionesCupones };
            this.PromocionesCuponesUsadas.Filter = i => {
                var item = (Promocion)i;
                return item.Used;
            };
            this.Pagos.CollectionChanged += Pagos_CollectionChanged;
            this.PropertyChanged += CajaViewModel_PropertyChanged;
            
            if (!IsInDesignMode)
            {
                _reports = new Helpers.ReportsHelper();
                _proxy = CommonServiceLocator.ServiceLocator.Current.GetInstance<Common.ServiceContracts.IDataServiceAsync>();
                _pproxy = CommonServiceLocator.ServiceLocator.Current.GetInstance<Common.ServiceContracts.ICommonServiceAsync>();
                _mapper = CommonServiceLocator.ServiceLocator.Current.GetInstance<AutoMapper.IMapper>();
                _client = new Helpers.ServiceClient();
            }
            
            this.RemovePagoCommand = new RelayCommand(/*async */() => {
                //_ls.RemovePago(this.SelectedPago.Id);
                this.Pagos.Remove(this.SelectedPago);
                //await this.UpdatePromociones();
            }, () => this.SelectedPago != null);

            this.RemoveCuponCommand = new RelayCommand(() => {
                var cupon = this.SelectedCupon;
                //_ls.RemoveCupon(cupon.Folio);
                var q = this.PromocionesCupones.OfType<PromocionCupon>().Where(i => i.Cupon == cupon.Folio).ToArray();
                foreach (var item in q)
                {
                    this.PromocionesCupones.Remove(item);
                }
                this.Cupones.Remove(this.SelectedCupon);
            }, () => this.SelectedCupon != null);

            this.SaleCommand = new RelayCommand(this.Sale, () => {
                if (this.SaleResponse == null
                        && this.Total > 0 && this.Remaining == 0)
                {                    
                    if (this.IsValid())
                    {
                        var pagado = this.Productos.All(i => i.Pagado);
                        if(pagado)
                            return true;
                    }
                }
                return false;
            });
            
            this.RemoveCommand = new RelayCommand(async () =>
            {
                await this.RemoveItem(this.SelectedItem);
            }, () => this.SelectedItem != null);
            this.AddCommand = new RelayCommand(async () => {
                //this.IsBusy = true;
                await Add();
                this.IsBusy = false;
            }, () => !string.IsNullOrWhiteSpace(this.SerieSearch));

            this.AddCuponCommand = new RelayCommand(() => {
                this.IsBusy = true;
                this.AddCuponHelper();
                this.IsBusy = false;
            }, () => !string.IsNullOrWhiteSpace(this.CuponSearch));

            this.LoadClienteCommand = new RelayCommand(() => {
                Messenger.Default.Send(
                            new Utilities.Messages.OpenModal
                            {
                                Name = Utilities.Constants.Modals.cliente,
                                GID = this.GID
                            });
            });
            this.LoadVendedorCommand = new RelayCommand(() => {
                Messenger.Default.Send(
                            new Utilities.Messages.OpenModal
                            {
                                Name = Utilities.Constants.Modals.vendedor,
                                GID = this.GID
                            });
            }, () => this.HasCalzado);
            this.RemoveVendedorCommand = new RelayCommand(() =>
            {
                this.Vendedor = null;
            }, () => this.Vendedor != null);
            this.AddDescuentoAdicional = new RelayCommand(() => {

                if (this.SelectedItem.DescuentoAdicional != null)
                    this.SelectedItem.DescuentoAdicional = null;
                else
                {
                    Messenger.Default.Send(
                                new Utilities.Messages.OpenModal
                                {
                                    Name = Utilities.Constants.Modals.descuento,
                                    GID = this.GID
                                });
                }
            }, () => this.SelectedItem != null);
            this.AddNotaCommand = new RelayCommand(() =>
            {
                Messenger.Default.Send(
                                new Utilities.Messages.OpenModalItem
                                {
                                    Name = Utilities.Constants.Modals.nota,                                    
                                    GID = this.GID,
                                    Item = this.SelectedItem
                                });
            }, () => this.SelectedItem != null);
            this.ClearClienteCommand = new RelayCommand(async () => {
                this.Cliente = null;
                this.NuevoCliente = null;
                _ls.ClearCliente();
                _skipPromociones = true;
                foreach (var item in this.PromocionesCupones.OfType<Common.Entities.PromocionCupon>().ToArray())
                {
                    if (item.Cliente.HasValue)
                    {
                        this.PromocionesCupones.Remove(item);
                        _ls.RemoveCupon(item.Cupon);
                    }
                }
                foreach (var item in this.Pagos.Where(i => i.ClientId.HasValue).ToArray())
                {
                    this.Pagos.Remove(item);
                    //_ls.RemovePago(item.Id);
                }
                this.ClientConfirmed = false;
                this.FormasPago.Refresh();
                _skipPromociones = false;
                await this.RefreshPromociones();
                this.SaleCommand.RaiseCanExecuteChanged();
            });

            MoveProductoCommand = new RelayCommand<Models.MoveDirection>(async dir => {
                this.Move(this.SelectedItem, this.Productos, dir, o => this.SelectedItem = o);
                await this.RefreshPromociones();
            }, dir => this.CanMove(this.SelectedItem, this.Productos, dir));
            
            MovePagoCommand = new RelayCommand<Models.MoveDirection>(async dir => {
                this.Move(this.SelectedPago, this.Pagos, dir, o => this.SelectedPago = (Models.Pagos.Pago)o);
                await this.RefreshPromociones();
            }, dir => this.CanMove(this.SelectedPago, this.Pagos, dir));

            MoveCuponCommand = new RelayCommand<Models.MoveDirection>(async dir => {
                this.Move(this.SelectedPromocion, this.PromocionesCupones, dir, o => this.SelectedPromocion = o);
                await this.RefreshPromociones();
            }, dir => this.CanMove(this.SelectedPromocion, this.PromocionesCupones, dir));
            
            this.PagarCommand = new RelayCommand(() => {

                this.ShowPagos = true;

            }, () => this.Total > 0 && this.Remaining > 0);


            this.ConfirmClienteCommand = new RelayCommand(() => { 
            


            });

            if (!this.IsInDesignMode)
            {                
                _scanner = CommonServiceLocator.ServiceLocator.Current.GetInstance<Utilities.Interfaces.IScanner>();
                if(_scanner != null)
                    _scanner.DataReceived += Scaner_DataReceived;                
            }

            this.PrintCommand = new RelayCommand(() => {

                _reports.Compra(this.Sucursal.Clave, this.SaleResponse.Folio);

            }, () => this.IsComplete);

            this.TestCommand = new RelayCommand<string>(p => {

                if (p == "start")
                {
                    _scanner?.Start();
                }
                if (p == "stop")
                {
                    _scanner?.Stop();
                }
            
            });
        }
        private void AddCuponHelper()
        {
            var cupon = _common.PrepareCupon(this.CuponSearch);

            //var item = this.PromocionesCupones.OfType<Common.Entities.PromocionCupon>().Where(i => i.Cupon == cupon).SingleOrDefault();
            var item = this.Cupones.Where(i => i.Folio == cupon).SingleOrDefault();
            if (item != null)
            {
                var res = MessageBox.Show("Este cupón ya ha sido agregado, desea removerlo?", "AVISO", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (res == MessageBoxResult.Yes)
                {
                    var remove = this.PromocionesCupones.OfType<Common.Entities.PromocionCupon>().Where(i => i.Cupon == cupon).ToList();
                    remove.ForEach(i => this.PromocionesCupones.Remove(i));
                    this.Cupones.Remove(item);
                    //_ls.RemoveCupon(cupon);
                    this.CuponSearch = null;
                }
            }
            else
            {
                var rcup = _proxy.FindCupon(cupon);
                if (rcup.Status != CuponStatus.Activo)
                {
                    MessageBox.Show($"cupon {cupon} {rcup.Status}");
                    return;
                }
                var cps = rcup?.Promociones;
                if (cps != null && cps.Any())
                {
                    this.Cupones.Add(rcup);

                    var added = false;
                    foreach (var cup in cps)
                    {
                        if (!cup.Cliente.HasValue || (cup.Cliente.HasValue && cup.Cliente == this.Cliente?.Id))
                        {
                            added = true;
                            cup.Enabled = true;
                            this.PromocionesCupones.Add(cup);
                        }
                    }
                    if (added)
                    {
                        //_ls.AddCupon(cupon);
                    }
                    this.CuponSearch = null;
                }
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
                        //TotalCalzado = this.RemainingCalzado,
                        //TotalElectronica = this.RemainingElectronica,
                        Caja = this,
                        ClientId = this.Cliente?.Id,
                        HasPromocionPlazo = this.Productos.Where(i => !i.MaxPlazos.HasValue).Any(),
                        ProductosPlazos = this.Productos.Where(i => i.MaxPlazos.HasValue && i.MaxPlazos > 0)
                    });
        }
        protected override void AddPago(Models.Pagos.Pago p)
        {
            //_skipPromociones = true;
            base.AddPago(p);
            //_skipPromociones = false;
            //_ls.AddPago(o, p.Id);

            //await this.UpdatePromociones();
            //this.UpdatePagos();                
            //await this.RefreshPromociones();

            //this.SaleCommand.RaiseCanExecuteChanged();
        }        

        public bool SkipPromociones
        {
            get => _skipPromociones;
            set => _skipPromociones = value;
        }
        private void Scaner_DataReceived(object sender, string e)
        {
            _scanner.Stop();
            //var sp = e.Split('|');
            //var gid = Guid.Parse(sp[1]);
            var gid = Guid.Parse(e);
            this.Cliente = _proxy.FindClienteByCode(gid);
            if (this.Cliente != null)
            {                
                this.NuevoCliente = new Models.NuevoCliente
                {
                    Id = this.Cliente.Id,
                    Nombre = this.Cliente.Nombre,
                    ApPaterno = this.Cliente.ApPaterno,
                    ApMaterno = this.Cliente.ApMaterno
                };
                //this.ClientConfirmed = true;
            }
        }
        private async Task RemoveItem(Models.Producto item)
        {
            await _client.ReleaseProductoAsync(item.Serie);
            item.PropertyChanged -= Prod_PropertyChanged;
            this.Productos.Remove(item);
            _ls.RemoveArticulo(item.Serie);
            //await this.GetPromociones();
        }
        private void AddItem(Models.Producto item, bool update = true)
        {
            item.PropertyChanged += Prod_PropertyChanged;
            this.Productos.Add(item);            
            if (update)
            {
                _ls.AddArticulo(item.Serie);
                //await this.GetPromociones();
            }
        }
        private void Move<T>(T selected, IList<T> items, Models.MoveDirection dir, Action<T> set)
        {
            if (!this.CanMove(selected, items, dir))
                return;

            if (dir == Models.MoveDirection.Up)
            {
                var item = selected;
                var ind = items.IndexOf(item);
                items.Remove(item);
                items.Insert(ind - 1, item);
                set(item);
            }
            if (dir == Models.MoveDirection.Down)
            {
                var ind = items.IndexOf(selected);
                var item = selected;
                items.Remove(item);
                items.Insert(ind + 1, item);
                set(item);
            }
        }
        private bool CanMove<T>(T selected, IList<T> items, Models.MoveDirection dir)
        {
            if (selected == null)
                return false;                        
            if (dir == Models.MoveDirection.Up)
            {
                var ind = items.IndexOf(selected);
                if (ind > 0)
                    return true;
            }
            if (dir == Models.MoveDirection.Down)
            {
                var ind = items.IndexOf(selected);
                if (ind < items.Count - 1)
                    return true;
            }
            return false;
        }

        protected override bool IsReady()
        {
            return this.SaleResponse != null;
        }
        

        protected override void RegisterMessages()
        {
            base.RegisterMessages();
            Messenger.Default.Register<Utilities.Messages.LogoutTimeout>(this, m =>
            {
                this.Clear(true);
            });            

            Messenger.Default.Register<Messages.DescuentoEspecial>(this, this.GID, m =>
            {
                if (m.Descuento != null)
                {
                    this.SelectedItem.DescuentoAdicional = m;
                }
            });
            Messenger.Default.Register<Messages.NotaItem>(this, this.GID, m =>
            {
                this.SelectedItem.Precio = m.Precio;
                this.SelectedItem.Notas = m.Razon;
                this.SelectedItem.NotaRazon = m.TipoRazon;
            });
            Messenger.Default.Register<Messages.ClienteMessage>(this, this.GID, async m => {
                this.Cliente = m.Cliente;
                this.NuevoCliente = null;
                if (this.Cliente != null)
                {
                    _ls.AddCliente(m.Cliente.Id);
                    this.NuevoCliente = new Models.NuevoCliente
                    {
                        Id = this.Cliente.Id,
                        Nombre = this.Cliente.Nombre,
                        ApPaterno = this.Cliente.ApPaterno,
                        ApMaterno = this.Cliente.ApMaterno
                    };

                    var cupones = await _proxy.FindCuponesByClienteAsync(this.Cliente.Id.Value);
                    if (cupones != null)
                    {
                        foreach (var cup in cupones)
                        {
                            this.PromocionesCupones.Add(cup);
                            _ls.AddCupon(cup.Cupon);
                        }
                    }
                }
                this.FormasPago.Refresh();
                await this.RefreshPromociones();                
                this.SaleCommand.RaiseCanExecuteChanged();                     
            });
            Messenger.Default.Register<Messages.NuevoClienteMessage>(this, this.GID,async  m => {
                this.NuevoCliente = m.Cliente;
                this.Cliente = null;
                var nuevo = _mapper.Map<Utilities.Entities.NuevoCliente>(this.NuevoCliente);
                _ls.AddCliente(nuevo);
                await this.RefreshPromociones();
                this.SaleCommand.RaiseCanExecuteChanged();
            });

            Messenger.Default.Register<Messages.Vendedor>(this, this.GID, m => {
                this.Vendedor = m.Empleado;
                if (m.Empleado != null)
                {
                    _ls.UpdateVendedor(this.Vendedor.Id);                    
                }
            });

            Messenger.Default.Register<Utilities.Messages.ShortcutMessage>(this, this.GID, m => {
                switch (m.Key)
                {
                    case Key.F1:
                    case Key.F2:
                    case Key.F3:
                    case Key.F4:
                    case Key.F5:
                    case Key.F6:
                    case Key.F7:
                    case Key.F8:
                    case Key.F9:
                        var q = _formas.Where(i => i.Value.Key == m.Key);
                        if (q.Any())
                        {
                            var item = q.Single();
                            this.AddFormaCommand.Execute(item.Key);
                        }
                        break;
                    case Key.F10:
                        this.LoadClienteCommand.Execute(null);
                        break;
                    case Key.F11:
                        this.LoadVendedorCommand.Execute(null);
                        break;
                    case Key.F12:
                        this.AddDescuentoAdicional.Execute(null);
                        break;
                    //case Key.F12:
                    //    this.SaleCommand.Execute(null);
                    //    break;
                }
            });
        }

        private async void Sale()
        {            
            this.IsBusy = true;
            var sale = new SaleRequest
            {
                Tipo = this.Tipo,
                VendedorId = this.Vendedor?.Id,
                Productos = this.Productos.Select(
                    i => new SerieFormasPago 
                    { 
                        Serie = i.Serie, 
                        Precio = i.PrecioOriginal != i.Precio ? i.Precio : null,
                        FormasPago = i.FormasPago.Select(k => k.FormaPago),
                        Pagos = i.FormasPago.GroupBy(k => k.FormaPago).ToDictionary(k => k.Key, k => k.Sum(kk => kk.Importe)),
                        AdicionalId = i.DescuentoAdicional?.Descuento?.Id, 
                        AdicionalDesc = i.DescuentoAdicional?.Descripcion,
                        Notas = i.Notas,
                        NotaRazon = i.NotaRazon,
                        Promociones = i.FormasPago.GroupBy(k => k.FormaPago).ToDictionary(k => k.Key, k => k.First().HasPromocion)
                    }),
                Sucursal = this.Sucursal.Clave,
                PromocionesCupones = this.PromocionesCupones.Where(i => i.Enabled).Select(i => new PromocionCuponItem
                {
                    PromocionId = i.PromocionId,
                    Cupon = i.IsCupon ? ((PromocionCupon)i).Cupon : null
                })
            };
            sale.Cliente = Helpers.Parsers.PaseCliente(this.NuevoCliente, this.Cliente, this.Sucursal);
            //AQUI SALTA EL ERROR EN EL EJECUTABLE
            sale.Pagos = this.PreparePagos();
            this.SaleResponse = await _client.SaleAsync(sale);
            this.Clear(false);
            this.IsBusy = false;
            //MessageBox.Show($"ID: {this.Folio}");
        }

        
        private async void Clear(bool release)
        {
            _ls.Clear();
            if (release)
            {
                foreach (var item in this.Productos)
                {
                    await _client.ReleaseProductoAsync(item.Serie);
                }
            }
        }
        public override void Close()
        {
            this.Clear(true);   
        }
        protected override async void LoadData()
        {
            _skipPromociones = true;
            _ls = CommonServiceLocator.ServiceLocator.Current.GetInstance<Utilities.Interfaces.ILocalStorage>();
            _ls.SetGID(this.GID);
            var venta = _ls.LoadVenta(this.Cajero.Id);            
            if (!venta.VendedorId.HasValue)
            {
                //Messenger.Default.Send(
                //        new Messages.OpenModal
                //        {
                //            Name = Constants.Modals.vendedor,
                //            GID = this.GID,
                //            Close = true
                //        });
            }
            else
            {
                this.Vendedor = await _pproxy.FindVendedorAsync(venta.VendedorId.Value);
                foreach (var item in venta.Articulos)
                {
                    var res = await _proxy.ScanProductoAsync(item.Serie, this.Sucursal.Clave);
                    var prod = _mapper.Map<Models.Producto>(res.Producto);
                    AddItem(prod, false);
                }
                foreach (var item in venta.Cupones)
                {
                    var res = _proxy.FindCupon(item.Cupon);
                    foreach (var cup in res.Promociones)
                    {
                        this.PromocionesCupones.Add(cup);
                    }                    
                }
                foreach (var item in venta.Pagos)
                {
                    var m = Utilities.Helpers.Serializer.Deserialize<Utilities.Messages.Pago>(item.Data);
                    var p = await ParsePago(m, item.Id);
                    this.Pagos.Add(p);
                }
                if (venta.ClienteId.HasValue)
                {
                    this.Cliente = _proxy.FindCliente(venta.ClienteId.Value);
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
                else if (venta.NuevoCliente != null)
                {
                    var nuevo = Utilities.Helpers.Serializer.Deserialize<Utilities.Entities.NuevoCliente>(venta.NuevoCliente);
                    this.NuevoCliente = _mapper.Map<Models.NuevoCliente>(nuevo);                    
                }
            }

            _skipPromociones = false;
            //await GetPromociones();
            //await UpdatePromociones();
            await RefreshPromociones();
            this.SaleCommand.RaiseCanExecuteChanged();
            this.FormasPago.Refresh();
        }

        private async Task GetPromociones()
        {
            await GetPromocionesHelper();
        }
        private async Task<bool> GetPromocionesHelper()
        {
            //if (_skipPromociones)
            //    return;

            if (this.IsInDesignMode)
                return false;

            if (!this.Productos.Any())
            {
                var removes = this.PromocionesCupones.Where(i => !i.IsCupon).ToList();
                foreach (var item in removes)
                {
                    this.PromocionesCupones.Remove(item);
                }
                return false;
            }

            var request = new Common.Entities.CheckPromocionesRequest
            {
                Sucursal = this.Sucursal.Clave,
                Productos = this.Productos.Select(i => new SerieFormasPago
                {
                    Serie = i.Serie,
                    FormasPago = i.FormasPago.Select(k => k.FormaPago)
                }),
            };
            var res = await _proxy.GetPromocionesAsync(request);
            var current = this.PromocionesCupones.Where(i => !i.IsCupon).ToList();
            //this.PromocionesCupones.Remove(item);
            var edited = false;
            //var skip = _skipPromociones;
            //if (!skip)
            //    _skipPromociones = true;
            foreach (var item in res)
            {
                var q = this.PromocionesCupones.Where(i => !i.IsCupon && i.PromocionId == item.PromocionId);
                if (!q.Any())
                {
                    //item.Enabled = true;
                    this.PromocionesCupones.Add(item);
                    edited = true;
                }
                else
                {
                    var rem = current.Where(i => i.PromocionId == item.PromocionId).SingleOrDefault();
                    if (rem != null)
                    {
                        current.Remove(rem);
                        edited = true;
                    }
                }
            }
            //if (!skip)
            //    _skipPromociones = false;
            //if (edited && !_skipPromociones)
            //    await this.RefreshPromociones();
            return edited;
        }
        private static object syncUpdate = new object();
        public async Task UpdatePromociones()
        {
            if (_skipPromociones)
                return;
            //if (!force && Monitor.IsEntered(syncUpdate))
            //    return;
            Monitor.Enter(syncUpdate);
            await UpdatePromocionesHelper();
            Monitor.Exit(syncUpdate);
        }
        private async Task UpdatePromocionesHelper()
        {
            if (!this.PromocionesCupones.Any())
                return;

            if (this.IsInDesignMode)
                return;

            this.IsBusy = true;
            var request = new Common.Entities.CheckPromocionesCuponesRequest
            {
                //TODO formas pago promociones pagadas
                Sucursal = this.Sucursal.Clave,
                PromocionesCupones = this.PromocionesCupones.Where(i => i.Enabled).Select(i => new PromocionCuponItem
                {
                    PromocionId = i.PromocionId,
                    Cupon = i.IsCupon ? ((PromocionCupon)i).Cupon : null
                }),
                Productos = this.Productos.Select(i => new SerieFormasPago { 
                    Serie = i.Serie,
                    Precio = i.PrecioOriginal != i .Precio ? i.Precio : null,
                    //FormasPago = (i.Pagado || i.PagadoInit) && i.FormasPago.Count == 1 ? 
                    //    i.FormasPago.Select(k => k.FormaPago) : (IEnumerable<FormaPago>)new FormaPago[] { }
                    FormasPago = i.FormasPago.Select(k => k.FormaPago),
                    Pagos = i.FormasPago.GroupBy(k => k.FormaPago).ToDictionary(k => k.Key, k => k.Sum(kk => kk.Importe)),
                    Promociones = i.FormasPago.GroupBy(k => k.FormaPago).ToDictionary(k => k.Key, k => k.First().HasPromocion),
                }),
            };
            if (this.NuevoCliente != null)
            {
                request.HasCliente = true;
                request.ClienteId = this.Cliente?.Id;
            }
            
            //TODO restructurar los pagos considerando el init
            var res = await _proxy.CheckPromocionesAsync(request);
            this.PromocionesCupones.ToList().ForEach(i => i.Used = false);            
            foreach (var item in res.Promociones)
            {
                var p = this.Productos.Where(i => i.Serie == item.Serie).Single();
                p.DescuentoDirecto = item.Descuento;
                p.PromocionId = item.PromocionId;
                p.Fijo = item.Fijo;
                p.Index = item.Index;
                p.Monedero = item.Monedero;

                //if (save)
                //{
                //    p.Init = null;
                //    if (p.PromocionId.HasValue)
                //        p.Init = p.Descuento;
                //}
            }
            foreach (var item in res.Promociones.Where(i => i.PromocionId.HasValue))
            {                
                var p = this.PromocionesCupones.Where(i => i.PromocionId == item.PromocionId).FirstOrDefault();
                if (p != null)
                {
                    p.Used = true;
                    continue;
                }
            }
            this.IsBusy = false;
        }
                
        public override void UpdatePagos()
        {
            foreach (var item in this.Productos)
            {
                item.Pago = null;
                item.FormasPago.Clear();
            }
            foreach (var pago in this.Pagos)
            {
                var importe = pago.Importe.Value;
                foreach (var producto in this.Productos)
                {
                    if (pago is Models.Pagos.PagoVale)
                    {
                        var vale = (Models.Pagos.PagoVale)pago;
                        if (!(vale.Info?.Electronica ?? false) && producto.Electronica)
                            continue;
                    }
                    var pp = producto.Pago ?? 0;
                    if (producto.Saldo == 0)
                        continue;
                    if (producto.Saldo >= importe)
                    {
                        pp += importe;
                        producto.Pago = pp;
                        producto.FormasPago.Add(new FormaPagoImporte { 
                            ID = pago.Id, 
                            FormaPago = pago.FormaPago, 
                            Importe = importe, 
                            HasPromocion = pago.HasPromocion
                        });
                        break;
                    }
                    else 
                    {
                        var pg = producto.Saldo ?? 0;
                        importe -= pg;
                        pp += pg;
                        producto.Pago = pp;
                        producto.FormasPago.Add(new FormaPagoImporte { 
                            ID = pago.Id, 
                            FormaPago = pago.FormaPago, 
                            Importe = pg, 
                            HasPromocion = pago.HasPromocion
                        });
                    }
                }
            }
        }


        private async Task Add()
        {
            var ser = _common.PrepareSerie(this.SerieSearch);

            var q = this.Productos.Where(i => i.Serie == ser).SingleOrDefault();
            if (q != null)
            {
                if (this.SelectedItem == q)
                {
                    var res = MessageBox.Show($"La serie '{ser}' ya está registrada en la ventan\nDesea removerla?", "Confirmar", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (res == MessageBoxResult.Yes)
                    {
                        await RemoveItem(q);                        
                    }
                    this.SerieSearch = null;
                    return;
                }
                else
                {
                    this.SelectedItem = q;
                    this.SerieSearch = null;
                    return;
                }
            }
            //A PARTIR DE ESTA LINEA SE ESTÁ GENERANDO EL ERROR EN EL EJECUTABLE ¿POR QUÉ? QUIEN SABE xd
            var item = await _proxy.ScanProductoAsync(ser, this.Sucursal.Clave);
            if (item != null)
            {
                var valid = new Common.Constants.Status[] {
                        Common.Constants.Status.AC,
                        Common.Constants.Status.IF,
                        Common.Constants.Status.AB
                    };
                if (valid.Contains(item.Status) 
                    || (item.Status == Common.Constants.Status.CA && item.UsuarioCajaId == this.Cajero.Id))
                {
                    if (await _client.RequestProductoAsync(item.Producto.Serie))
                    {
                        var prod = _mapper.Map<Models.Producto>(item.Producto);
                        AddItem(prod);
                        this.SelectedItem = prod;

                        if (!prod.Electronica && this.Vendedor == null)
                        {                            
                            Messenger.Default.Send(
                                    new Utilities.Messages.OpenModal
                                    {
                                        Name = Utilities.Constants.Modals.vendedor,
                                        GID = this.GID
                                    });
                        }
                    }                    
                }
                else
                    MessageBox.Show($"{item.Producto.Serie} - {item.Status}");
                this.SerieSearch = null;
            }
            else
                MessageBox.Show($"El numero de serie: {ser} no existe.");
        }

        private void Prod_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(Models.Producto.Precio):
                    this.RaisePropertyChanged(nameof(this.SubTotal));
                    break;
            }
        }

        public void Dispose()
        {
            
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var formas = _formas.Where(i => i.Value.ClientRequired).Select(i => i.Key).ToArray();
            if (this.Pagos.Where(i => formas.Contains(i.FormaPago)).Any())
            {
                if (this.NuevoCliente == null &&
                    this.Cliente == null)
                {
                    yield return new ValidationResult("Cliente Requerido");
                }
            }

            if (this.HasCalzado && this.Vendedor == null)
                yield return new ValidationResult("Vendedor Requerido");
            if (!this.HasCalzado && this.Vendedor != null)
                yield return new ValidationResult("Vendedor No Requerido");
        }
    }
}

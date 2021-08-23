using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using SirCoPOS.Common.Constants;
using SirCoPOS.Common.Entities;
using SirCoPOS.Utilities.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace SirCoPOS.Client.Helpers
{
    public abstract class TabsPagosViewModel : Helpers.TabViewModelBase, ICajaBase
    {
        protected IDictionary<FormaPago, Models.FormaPagoKey> _formas;
        private readonly Common.ServiceContracts.IDataServiceAsync _proxy;
        public TabsPagosViewModel()
        {
            this.PropertyChanged += TabsPagosViewModel_PropertyChanged;
            if (!this.IsInDesignMode)
            {
                _proxy = CommonServiceLocator.ServiceLocator.Current.GetInstance<Common.ServiceContracts.IDataServiceAsync>();
            }
            this.Pagos = new ObservableCollection<Utilities.Interfaces.IPagoItem>();
            this.Pagos.CollectionChanged += Pagos_CollectionChanged;
            _formas = new Dictionary<FormaPago, Models.FormaPagoKey>() {
                { FormaPago.EF, new Models.FormaPagoKey { Enabled = true, Key = Key.F1, Duplicate = false, WithClient = false, ClientRequired = false, Credito = false } },
                { FormaPago.TC, new Models.FormaPagoKey { Enabled = true, Key = Key.F2, Duplicate = false, WithClient = false, ClientRequired = false, Credito = false } },
                { FormaPago.TD, new Models.FormaPagoKey { Enabled = true, Key = Key.F3, Duplicate = false, WithClient = false, ClientRequired = false, Credito = false } },
                { FormaPago.DV, new Models.FormaPagoKey { Enabled = true, Key = Key.F4, Duplicate = false, WithClient = false, ClientRequired = false, Credito = false } },
                { FormaPago.MD, new Models.FormaPagoKey { Enabled = true, Key = Key.F10, Duplicate = false, WithClient = false, ClientRequired = false, Credito = false } },

                { FormaPago.VA, new Models.FormaPagoKey { Enabled = true, Key = Key.F5, Duplicate = false, WithClient = false, ClientRequired = true, Credito = true } },
                { FormaPago.CV, new Models.FormaPagoKey { Enabled = true, Key = Key.F6, Duplicate = false, WithClient = false, ClientRequired = false, Credito = true } },
                { FormaPago.CP, new Models.FormaPagoKey { Enabled = true, Key = Key.F7, Duplicate = false, WithClient = false, ClientRequired = true, Credito = true } },
                { FormaPago.CD, new Models.FormaPagoKey { Enabled = true, Key = Key.F8, Duplicate = false, WithClient = false, ClientRequired = true, Credito = true } },
                { FormaPago.VD, new Models.FormaPagoKey { Enabled = true, Key = Key.F9, Duplicate = false, WithClient = false, ClientRequired = true, Credito = true } },
                { FormaPago.VE, new Models.FormaPagoKey { Enabled = true, Key = Key.F10, Duplicate = false, WithClient = false, ClientRequired = true, Credito = true } },
                //{ FormaPago.CI, new Models.FormaPagoKey { Enabled = true, Key = Key.F12, Duplicate = false, WithClient = false, ClientRequired = false, Credito = true } },
                //{ FormaPago.VS, new Models.FormaPagoKey { Enabled = true, Key = Key.J, Duplicate = false, WithClient = false, ClientRequired = false, Credito = true } }
            };
            this.FormasPago = CollectionViewSource.GetDefaultView(_formas);
            this.FormasPago.Filter = k => {
                var item = (KeyValuePair<FormaPago, Models.FormaPagoKey>)k;

                if (item.Value.Credito)
                {
                    if (this.Pagos.Where(i => _formas[i.FormaPago].Credito).Any())
                        return false;
                }
                if (!item.Value.Duplicate)
                {
                    //Filtrar que no aparezca una forma de pago que ya exista.
                    if (this.Pagos.Where(i => i.FormaPago == item.Key).Any())
                        return false;
                }
                if (item.Value.WithClient)
                {
                    if ((this.Cliente?.Id.HasValue ?? false) && this.ClientConfirmed)
                        return item.Value.Enabled;
                    return false;
                }
                return item.Value.Enabled;
            };
            //=========================================== ESTO OMITE LA EJECUCION DE CAJA Y CAMBIO ===========================
            //if (!IsInDesignMode)
            //{
            //    var proxy = CommonServiceLocator.ServiceLocator.Current.GetInstance<Common.ServiceContracts.IDataServiceAsync>();
            //    var formas = proxy.GetFormasPago();
            //    foreach (var item in _formas)
            //    {
            //        item.Value.Enabled = false;
            //    }
            //    foreach (var item in formas)
            //    {
            //        _formas[item].Enabled = true;
            //    }
            //}
            //================================================================================================================

            this.AddFormaCommand = new RelayCommand<FormaPago>(fp => {
                this.ShowPagos = false;
                this.OpenFormaPago(fp);                
            }, p => {
                var q = this._formas.Where(i => i.Key == p);
                return q.Any() && this.Total > 0 && this.Remaining > 0 && q.Single().Value.Enabled;
            });

        }

        private void TabsPagosViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(this.Total):
                    this.RaisePropertyChanged(nameof(this.TotalPayment));
                    this.RaisePropertyChanged(nameof(this.Remaining));
                    break;
                case nameof(this.Remaining):
                    this.RaisePropertyChanged(nameof(this.RemainingCalzado));
                    this.RaisePropertyChanged(nameof(this.RemainingElectronica));
                    break;
                case nameof(this.ClientConfirmed):
                    this.FormasPago.Refresh();
                    break;
            }
        }

        private void Pagos_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    {
                        //var items = e.NewItems.OfType<Models.Pagos.Pago>();
                        //foreach (var item in items)
                        //{
                        //    if (!_formas[item.FormaPago].Duplicate)
                        //    {
                        //        _formas[item.FormaPago].Enabled = false;
                        //    }

                        //    if (_formas[item.FormaPago].Credito)
                        //    {
                        //        _formas.Where(i => i.Value.Credito).ToList().ForEach(i => i.Value.Enabled = false);
                        //    }
                        //}
                        this.FormasPago.Refresh();
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    {
                        //var items = e.OldItems.OfType<Models.Pagos.Pago>();
                        //foreach (var item in items)
                        //{
                        //    if (!_formas[item.FormaPago].Duplicate)
                        //    {
                        //        _formas[item.FormaPago].Enabled = true;
                        //    }

                        //    if (_formas[item.FormaPago].Credito)
                        //    {
                        //        if (!this.Pagos.Where(i => _formas[i.FormaPago].Credito).Any())
                        //        {
                        //            var q = _formas.Where(i => i.Value.Credito).ToList();
                                    
                        //        }                                    
                        //    }
                        //}
                        this.FormasPago.Refresh();
                    }
                    break;
            }

            this.RaisePropertyChanged(nameof(this.Remaining));
            this.RaisePropertyChanged(nameof(this.TotalPayment));
            //await this.UpdatePromociones();            
            this.AddFormaCommand.RaiseCanExecuteChanged();
            //this.SaleCommand.RaiseCanExecuteChanged();
        }

        protected override void RegisterMessages()
        {            
            Messenger.Default.Register<Utilities.Messages.Pago>(this, this.GID, async o => {
                //if (!_formas[o.FormaPago].Duplicate)
                //{
                //    _formas[o.FormaPago].Enabled = false;
                //    this.FormasPago.Refresh();
                //}

                var p = await ParsePago(o);
                p.PropertyChanged += (s, e) => RaisePropertyChanged(nameof(this.TotalPayment));

                AddPago(p);

            });
        }
        protected virtual void AddPago(Models.Pagos.Pago p)
        {
            this.Pagos.Add(p);
        }
        protected async Task<Models.Pagos.Pago> ParsePago(Utilities.Messages.Pago o, Guid? id = null)
        {
            Models.Pagos.Pago p = null;
            if(o.FormaPago.ToString() == "CD")
            {
                Common.Constants.ClienteDato.opcion = 1;
            }
            else
            {
                Common.Constants.ClienteDato.opcion = 0;
            }
            switch (o.FormaPago)
            {
                case FormaPago.EF:
                    p = new Models.Pagos.Pago
                    {
                        FormaPago = o.FormaPago,
                        Importe = o.Importe,
                        Efectivo = o.Efectivo,
                    };
                    break;
                case FormaPago.MD:
                    p = new Models.Pagos.Pago
                    {
                        FormaPago = o.FormaPago,
                        Importe = o.Importe,
                        ClientId = o.Cliente
                    };
                    break;
                case FormaPago.DV:
                    p = new Models.Pagos.PagoDevolucion
                    {
                        FormaPago = o.FormaPago,
                        Importe = o.Importe,
                        Sucursal = o.Sucursal,
                        Folio = o.Folio
                    };
                    break;
                case FormaPago.TC:
                case FormaPago.TD:
                    p = new Models.Pagos.PagoTarjeta
                    {
                        FormaPago = o.FormaPago,
                        Importe = o.Importe,
                        Terminacion = o.Terminacion,
                        Referencia = o.Referencia
                    };
                    break;
                case FormaPago.VA:
                    {
                        var pv = new Models.Pagos.PagoVale
                        {
                            FormaPago = o.FormaPago,
                            Importe = o.Importe,
                            Vale = o.Vale,
                            Plazos = o.Plazos,
                            Promociones = o.Promociones,
                            SelectedPlazo = o.SelectedPlazo,
                            SelectedPromocion = o.SelectedPromocion,
                            ContraVale = o.ContraVale,
                            Limite = o.Limite,
                            ClientId = o.Cliente,
                            ProductosPlazos = o.PlazosProductos
                        };

                        if (!this.IsInDesignMode)
                        {
                            var promos = await _proxy.FindPromocionesValeAsync(this.Sucursal.Clave);
                            //pv.Plazos = promos.Plazos;
                            pv.Promociones = promos.Promociones;

                            var vale = await _proxy.FindValeAsync(o.Vale);
                            pv.Info = new Models.Pagos.PagoValeInfo
                            {
                                Distribuidor = vale.Distribuidor.Nombre,
                                Promocion = vale.Distribuidor.Promocion,
                                Electronica = vale.Distribuidor.Electronica
                            };

                            if (o.Cliente.HasValue)
                            {
                                var cli = _proxy.FindCliente(o.Cliente.Value);
                                Messenger.Default.Send(new Messages.ClienteMessage { Cliente = cli }, this.GID);
                            }
                        }
                        p = pv;
                    }
                    break;
                case FormaPago.VD:
                    {
                        var pv = new Models.Pagos.PagoVale
                        {
                            FormaPago = o.FormaPago,
                            Importe = o.Importe,
                            Vale = o.Vale,
                            Plazos = o.Plazos,
                            Promociones = o.Promociones,
                            SelectedPlazo = o.SelectedPlazo,
                            SelectedPromocion = o.SelectedPromocion,
                            ClientId = o.Cliente,
                            ProductosPlazos = o.PlazosProductos,
                        };

                        if (!this.IsInDesignMode)
                        {
                            var promos = await _proxy.FindPromocionesValeAsync(this.Sucursal.Clave);
                            //pv.Plazos = promos.Plazos;
                            pv.Promociones = promos.Promociones;

                            var vale = await _proxy.FindValeDigitalAsync(o.Vale);
                            pv.Info = new Models.Pagos.PagoValeInfo
                            {
                                Distribuidor = vale.Distribuidor.Nombre,
                                Promocion = vale.Distribuidor.Promocion,
                                Electronica = vale.Distribuidor.Electronica
                            };

                            if (o.Cliente.HasValue)
                            {
                                var cli = _proxy.FindCliente(o.Cliente.Value);
                                Messenger.Default.Send(new Messages.ClienteMessage { Cliente = cli }, this.GID);
                            }
                        }
                        p = pv;
                    }
                    break;
                case FormaPago.VE:
                    {
                        var pv = new Models.Pagos.PagoVale
                        {
                            FormaPago = o.FormaPago,
                            Importe = o.Importe,
                            Vale = o.Vale,
                            Negocio = o.Negocio,
                            NoCuenta = o.NoCuenta,
                            Plazos = o.Plazos,
                            Promociones = o.Promociones,
                            SelectedPlazo = o.SelectedPlazo,
                            SelectedPromocion = o.SelectedPromocion,
                            ClientId = o.Cliente,
                            ProductosPlazos = o.PlazosProductos
                        };

                        if (!this.IsInDesignMode)
                        {
                            var promos = await _proxy.FindPromocionesValeAsync(this.Sucursal.Clave);
                            //pv.Plazos = promos.Plazos;
                            pv.Promociones = promos.Promociones;

                            var vale = await _proxy.FindDistribuidorIdAsync(o.DistribuidorId);
                            pv.Info = new Models.Pagos.PagoValeInfo
                            {
                                Distribuidor = vale.Distribuidor.Nombre,
                                Promocion = vale.Distribuidor.Promocion,
                                Electronica = vale.Distribuidor.Electronica
                            };
                            //var vale = await _proxy.FindValeDigitalAsync(o.Vale);
                            //pv.Info = new Models.Pagos.PagoValeInfo
                            //{
                            //    Distribuidor = vale.Distribuidor.Nombre,
                            //    Promocion = vale.Distribuidor.Promocion,
                            //    SoloCalzado = vale.Distribuidor.SoloCalzado
                            //};

                            //if (o.Cliente.HasValue)
                            //{
                            //    var cli = _proxy.FindCliente(o.Cliente.Value);
                            //    Messenger.Default.Send(new Messages.ClienteMessage { Cliente = cli }, this.GID);
                            //}
                        }
                        p = pv;
                    }
                    break;
                case FormaPago.CP:
                    {
                        var cp = new Models.Pagos.PagoCredito
                        {
                            FormaPago = o.FormaPago,
                            Importe = o.Importe,
                            Vale = o.Vale,
                            Plazos = o.Plazos,
                            Promociones = o.Promociones,
                            SelectedPlazo = o.SelectedPlazo,
                            SelectedPromocion = o.SelectedPromocion,
                            ContraVale = o.ContraVale,
                            Limite = o.Limite,
                            ClientId = o.Cliente,
                            ProductosPlazos = o.PlazosProductos,
                        };

                        if (!this.IsInDesignMode)
                        {
                            var promos = await _proxy.FindPromocionesValeAsync(this.Sucursal.Clave);
                            //pv.Plazos = promos.Plazos;
                            cp.Promociones = promos.Promociones;

                            var vale = await _proxy.FindTarjetahabienteAsync(o.Vale);
                            cp.Info = new Models.Pagos.PagoValeInfo
                            {
                                Distribuidor = vale.Distribuidor.Nombre,
                                Promocion = vale.Distribuidor.Promocion,
                                Electronica = vale.Distribuidor.Electronica
                            };

                            if (o.Cliente.HasValue)
                            {
                                var cli = _proxy.FindCliente(o.Cliente.Value);
                                Messenger.Default.Send(new Messages.ClienteMessage { Cliente = cli }, this.GID);
                            }
                            else
                            {
                                this.Cliente = new Cliente { DistribuidorId = o.DistribuidorId };
                            }
                        }
                        p = cp;
                    }
                    break;
                case FormaPago.CD:
                    {
                        var cp = new Models.Pagos.PagoCredito
                        {
                            FormaPago = o.FormaPago,
                            Importe = o.Importe,
                            Vale = o.Vale,
                            Plazos = o.Plazos,
                            Promociones = o.Promociones,
                            SelectedPlazo = o.SelectedPlazo,
                            SelectedPromocion = o.SelectedPromocion,
                            ContraVale = o.ContraVale,
                            Limite = o.Limite,
                            ClientId = o.Cliente,
                            ProductosPlazos = o.PlazosProductos,
                        };

                        if (!this.IsInDesignMode)
                        {
                            var promos = await _proxy.FindPromocionesValeAsync(this.Sucursal.Clave);
                            //pv.Plazos = promos.Plazos;
                            cp.Promociones = promos.Promociones;

                            var vale = await _proxy.FindDistribuidorAsync(o.Vale);
                            cp.Info = new Models.Pagos.PagoValeInfo
                            {
                                Distribuidor = vale.Distribuidor.Nombre,
                                Promocion = vale.Distribuidor.Promocion,
                                Electronica = vale.Distribuidor.Electronica
                            };

                            if (o.Cliente.HasValue)
                            {
                                var cli = _proxy.FindCliente(o.Cliente.Value);
                                Messenger.Default.Send(new Messages.ClienteMessage { Cliente = cli }, this.GID);
                            }
                            else
                            {
                                this.Cliente = new Cliente { DistribuidorId = o.DistribuidorId };
                            }
                        }
                        p = cp;
                    }
                    break;
                case FormaPago.CV:
                    var cv = new Models.Pagos.PagoContraVale
                    {
                        FormaPago = o.FormaPago,
                        Importe = o.Importe,
                        Vale = o.Vale,
                        Plazos = o.Plazos,
                        Promociones = o.Promociones,
                        SelectedPlazo = o.SelectedPlazo,
                        SelectedPromocion = o.SelectedPromocion,
                        ContraVale = o.ContraVale,
                        Limite = o.Limite,
                        Sucursal = o.Sucursal,
                        ClientId = o.Cliente,
                        ProductosPlazos = o.PlazosProductos
                    };

                    if (!this.IsInDesignMode)
                    {
                        var promos = await _proxy.FindPromocionesValeAsync(this.Sucursal.Clave);
                        //pv.Plazos = promos.Plazos;
                        cv.Promociones = promos.Promociones;

                        var vale = await _proxy.FindContraValeAsync(o.Sucursal, o.Vale);
                        cv.Info = new Models.Pagos.PagoValeInfo
                        {
                            Distribuidor = vale.Distribuidor.Nombre,
                            Promocion = vale.Distribuidor.Promocion,
                            Electronica = vale.Distribuidor.Electronica
                        };

                        if (o.Cliente.HasValue)
                        {
                            var cli = _proxy.FindCliente(o.Cliente.Value);
                            Messenger.Default.Send(new Messages.ClienteMessage { Cliente = cli }, this.GID);
                        }
                    }
                    p = cv;
                    break;
                default:
                    throw new NotSupportedException();
            }
            p.Id = id ?? Guid.NewGuid();
            return p;
        }

        protected virtual void OpenFormaPago(FormaPago fp) 
        {
            Messenger.Default.Send(
                    new Utilities.Messages.OpenPago
                    {
                        GID = this.GID,
                        FormaPago = fp,
                        //Total = this.Remaining,
                        Caja = this,
                        ClientId = this.Cliente?.Id,
                        //ProductosPlazos = this.Productos.Where(i => i.MaxPlazos.HasValue && i.MaxPlazos > 0)
                    });
        }
        protected IEnumerable<Common.Entities.Pago> PreparePagos()
        {
            var pagos = new List<Common.Entities.Pago>();
            foreach (var item in this.Pagos.Where(i => (i.Importe ?? 0) > 0))
            {
                if (item is Models.Pagos.PagoDevolucion)
                {
                    var i = (Models.Pagos.PagoDevolucion)item;
                    pagos.Add(new Common.Entities.Pago
                    {
                        FormaPago = item.FormaPago,
                        Importe = item.Importe.Value,
                        Sucursal = i.Sucursal,
                        Devolucion = i.Folio
                    });
                }
                else if (item is Models.Pagos.PagoTarjeta)
                {
                    var i = (Models.Pagos.PagoTarjeta)item;
                    pagos.Add(new Common.Entities.Pago
                    {
                        FormaPago = item.FormaPago,
                        Importe = item.Importe.Value,
                        Terminacion = i.Terminacion,
                        Referencia = i.Referencia
                    });
                }
                else if (item is Models.Pagos.PagoContraVale)
                {
                    var i = (Models.Pagos.PagoContraVale)item;
                    pagos.Add(new Common.Entities.Pago
                    {
                        FormaPago = item.FormaPago,
                        Importe = item.Importe.Value,
                        Vale = i.Vale,
                        Plazos = i.SelectedPlazo,
                        FechaAplicar = i.SelectedPromocion,
                        Sucursal = i.Sucursal,
                        ProductosPlazos = i.ProductosPlazos,
                    });
                }
                else if (item is Models.Pagos.PagoCredito)
                {
                    var i = (Models.Pagos.PagoCredito)item;
                    pagos.Add(new Common.Entities.Pago
                    {
                        FormaPago = item.FormaPago,
                        Importe = item.Importe.Value,
                        Distribuidor = i.Vale,
                        //Vale = i.Vale,
                        Plazos = i.SelectedPlazo,
                        FechaAplicar = i.SelectedPromocion,
                        //ContraVale = i.ContraVale,
                        //Limite = i.Limite
                        ProductosPlazos = i.ProductosPlazos,
                    });
                }
                else if (item is Models.Pagos.PagoVale)
                {
                    var i = (Models.Pagos.PagoVale)item;
                    pagos.Add(new Common.Entities.Pago
                    {
                        FormaPago = item.FormaPago,
                        Importe = item.Importe.Value,
                        Vale = i.Vale,
                        Plazos = i.SelectedPlazo,
                        FechaAplicar = i.SelectedPromocion,
                        ContraVale = i.ContraVale,
                        Limite = i.Limite,
                        ProductosPlazos = i.ProductosPlazos,

                        Negocio = i.Negocio,
                        NoCuenta = i.NoCuenta
                    });
                }
                else
                {
                    pagos.Add(new Common.Entities.Pago
                    {
                        FormaPago = item.FormaPago,
                        Importe = item.Importe.Value,
                        Efectivo = (decimal) item.Efectivo,
                    });
                }
            }
            return pagos;
        }
        public ICollectionView FormasPago { get; set; }
        private bool _showPagos;
        public bool ShowPagos
        {
            get => _showPagos;
            set => this.Set(nameof(ShowPagos), ref _showPagos, value);
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
        private bool _clientConfirmed;
        public bool ClientConfirmed
        {
            get => _clientConfirmed;
            set => this.Set(nameof(this.ClientConfirmed), ref _clientConfirmed, value);
        }
        public RelayCommand<FormaPago> AddFormaCommand { get; private set; }

        public virtual decimal Total { get; set; }
        private decimal _totalpayment;
        public decimal TotalPayment
        {
            get { return this.Pagos.Where(i => i.Importe.HasValue).Sum(i => i.Importe.Value); }
            set { this.Set(nameof(this.TotalPayment), ref _totalpayment, value); }
        }
        public decimal Remaining
        {
            get { return this.Total - this.TotalPayment; }
        }
        public virtual decimal RemainingCalzado { get => 0; }
        public virtual decimal RemainingElectronica { get => 0; }
        private Utilities.Interfaces.IPagoItem _selectedPago;
        public Utilities.Interfaces.IPagoItem SelectedPago
        {
            get => _selectedPago;
            set => this.Set(nameof(this.SelectedPago), ref _selectedPago, value);
        }
        public ObservableCollection<Utilities.Interfaces.IPagoItem> Pagos { get; set; }
        public abstract void UpdatePagos();
    }
}

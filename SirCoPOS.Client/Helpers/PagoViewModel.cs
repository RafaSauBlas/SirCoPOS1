using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using RSG;
using SirCoPOS.Utilities.Interfaces;
using SirCoPOS.Utilities.Models;

namespace SirCoPOS.Client.Helpers
{
    public abstract class PagoViewModel : Helpers.ModalViewModelBase<Utilities.Messages.Pago>, Utilities.Interfaces.IPago
    {
        public abstract string Title { get; }
        public PagoViewModel()
        {
            this.DevFolio = null;
            this.DevSucursal = null;
            this.DevProrrateo =  null;
            this.PropertyChanged += PagoViewModel_PropertyChanged;
            _skip = false;
            this.Productos = new ObservableCollection<ProductoPlazoOpciones>();
            this.Ready = new RSG.Promise();
        }        
        public Promise Ready { get; private set; }        
        private bool _skip;
        private async void PagoViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (this.SkipPropertyChanged)
                return;

            switch (e.PropertyName)
            {
                case nameof(this.ClientId):
                    this.RaisePropertyChanged(nameof(this.NoClient));
                    break;
                case nameof(this.Total):
                    this.RaisePropertyChanged(nameof(this.Pendiente));
                    break;
                case "SelectedPromocion":
                    if (!_skip && this.Caja != null)
                    {
                        this.PagoIem.HasPromocion = this.HasSelectedPromocion;

                        this.Caja.UpdatePagos();//update pagos
                        if (this.Caja is ICaja)
                        {
                            await ((ICaja)this.Caja).UpdatePromociones();
                            this.Caja.UpdatePagos();
                        }

                        this.Total = this.PagoIem.Importe.Value + this.Caja.Remaining;
                        this.RaisePropertyChanged(nameof(this.Total));
                    }
                    this.AcceptCommand.RaiseCanExecuteChanged();
                    break;
                case nameof(this.Pagar):
                    this.RaisePropertyChanged(nameof(this.Pendiente));
                    if (!_skip && this.Caja != null)
                    {
                        this.PagoIem.Importe = this.Pagar;

                        this.Caja.UpdatePagos();//update pagos
                        if (this.Caja is ICaja)
                        {
                            await ((ICaja)this.Caja).UpdatePromociones();
                            this.Caja.UpdatePagos();
                        }

                        this.Total = this.PagoIem.Importe.Value + this.Caja.Remaining;
                        this.RaisePropertyChanged(nameof(this.Total));
                    }
                    Caja.refreshValorDV();
                    this.AcceptCommand.RaiseCanExecuteChanged();
                    break;
                case nameof(this.DevProrrateo):
                case nameof(this.DevFolio):
                    _skip = true;
                    var caja1 = (ICaja)this.Caja;

                    //  Replicar en caja la devolucion para actualizar las promociones
                    caja1.DevFolio = this.DevFolio;
                    caja1.DevSucursal = this.DevSucursal;

                    await caja1.UpdatePromociones();
                    this.Caja.UpdatePagos();
               
                    var yaPagado = Caja.Pagos.Where(i => i.FormaPago != Common.Constants.FormaPago.DV).Sum(i => i.Importe);
                    this.Pagar = this.Caja.Total - yaPagado;
                    this.Total = this.Pagar?? 0;

                    this.PagoIem.Importe = this.Total;

                    Caja.refreshValorDV();
                    this.Caja.UpdatePagos();
                    _skip = false;

                    break;
                case nameof(Caja):
                    if (this.Caja != null && !(this.Caja is ICaja))
                    {
                        var pago = new Models.Pagos.Pago
                        {
                            Id = Guid.NewGuid(),
                            FormaPago = this.FormaPago,
                            Importe = this.Caja.Remaining
                        };
                        if (pago.FormaPago == Common.Constants.FormaPago.VA
                            || pago.FormaPago == Common.Constants.FormaPago.VE)
                        {
                            var tmp = pago;
                            pago = new Models.Pagos.PagoVale
                            {
                                Id = tmp.Id,
                                FormaPago = tmp.FormaPago,
                                Importe = tmp.Importe,
                                Info = new Models.Pagos.PagoValeInfo
                                {
                                    Electronica = true
                                }
                            };
                        }
                        this.PagoIem = pago;
                        this.Caja.Pagos.Add(pago);
                        this.Caja.UpdatePagos();
                        this.Total = pago.Importe ?? 0;
                        this.Pagar = pago.Importe ?? 0;
                        //this.Caja.UpdatePagos();

                        this.Init();
                    }
                    if (this.Caja != null && this.Caja is ICaja)
                    {
                        var caja = (ICaja)this.Caja;
                        _skip = true;
                        var pago = new Models.Pagos.Pago { 
                            Id = Guid.NewGuid(),
                            FormaPago = this.FormaPago, 
                            Importe = this.Caja.Remaining 
                        };
                        if (pago.FormaPago == Common.Constants.FormaPago.VA
                            || pago.FormaPago == Common.Constants.FormaPago.VE)
                        {
                            var tmp = pago;
                            pago = new Models.Pagos.PagoVale
                            {
                                Id = tmp.Id,
                                FormaPago = tmp.FormaPago,
                                Importe = tmp.Importe,
                                Info = new Models.Pagos.PagoValeInfo {
                                    Electronica = true
                                }
                            };
                        }
                        this.PagoIem = pago;

                        var tc = this.Caja.RemainingCalzado;
                        var te = this.Caja.RemainingElectronica;

                        caja.SkipPromociones = true;
                        this.Caja.Pagos.Add(pago);
                        caja.SkipPromociones = false;
                        //await this.Caja.UpdatePromociones(save: true, force: true);

                        await caja.UpdatePromociones();
                        this.Caja.UpdatePagos();

                        caja.SkipPromociones = true;
                        this.Caja.Pagos.Remove(pago);
                        caja.SkipPromociones = false;
                        this.Pagar = this.Caja.Remaining;
                        pago.Importe = this.Caja.Remaining;

                        tc = this.Caja.RemainingCalzado;
                        te = this.Caja.RemainingElectronica;

                        caja.SkipPromociones = true;
                        this.Caja.Pagos.Add(pago);
                        caja.SkipPromociones = false;
                        this.Total = pago.Importe ?? 0;
                        this.Pagar = pago.Importe;

                        this.TotalCalzado = tc;
                        this.TotalElectronica = te;

                        await caja.UpdatePromociones();
                        this.Caja.UpdatePagos();

                        this.Init();
                        _skip = false;
                    }
                    break;
            }
        }
        protected async override Task<Utilities.Messages.Pago> Prepare(bool acept)
        {
            //if(this.Caja is ICaja)
            //    ((ICaja)this.Caja).SkipPromociones = true;
            this.Caja.Pagos.Remove(this.PagoIem);
            //if (this.Caja is ICaja)
            //    ((ICaja)this.Caja).SkipPromociones = false;
            //this.Caja.UpdatePagos();
            //await this.Caja.UpdatePromociones();
            //this.Caja.UpdatePagos();
            return await Task.FromResult(default(Utilities.Messages.Pago));
        }
        public async void ActualizaPromociones()
        {
            var caja1 = (ICaja)this.Caja;

            //  Replicar en caja la devolucion para actualizar las promociones
            caja1.DevFolio = this.DevFolio;
            caja1.DevSucursal = this.DevSucursal;

            await caja1.UpdatePromociones();
            this.Caja.UpdatePagos();

            var yaPagado = Caja.Pagos.Where(i => i.FormaPago != Common.Constants.FormaPago.DV).Sum(i => i.Importe);
            //this.Pagar = this.Caja.Total - yaPagado;
            //this.Total = this.Pagar ?? 0;

            //this.PagoIem.Importe = this.Total;
            await caja1.UpdatePromociones();
            //Caja.refreshValorDV();
            this.Caja.UpdatePagos();
        }
        protected Models.Pagos.Pago PagoIem { get; private set; }        
        protected virtual void Init() 
        {
            this.Ready.Resolve();
        }
        public abstract Common.Constants.FormaPago FormaPago { get; }

        private string _devprorrateo;
        public string DevProrrateo
        {
            get { return _devprorrateo; }
            set { this.Set(nameof(this.DevProrrateo), ref _devprorrateo, value); }
        }

        private decimal _total;
        public decimal Total
        {
            get { return _total; }
            set { this.Set(nameof(this.Total), ref _total, value); }
            //get => this.Caja?.Remaining ?? 0;
        }
        private decimal _TotalCalzado;
        public decimal TotalCalzado
        {
            get => _TotalCalzado;
            set => this.Set(nameof(TotalCalzado), ref _TotalCalzado, value);
        }
        private decimal _TotalElectronica;
        public decimal TotalElectronica
        {
            get => _TotalElectronica;
            set => this.Set(nameof(TotalElectronica), ref _TotalElectronica, value);
        }
        private decimal? _pagar;
        [Required]        
        public decimal? Pagar
        {
            get { return _pagar; }
            set { this.Set(nameof(this.Pagar), ref _pagar, value); }
        }
        public decimal? Pendiente { get { return this.Total - (this.Pagar ?? 0); } }

        private ICajaBase _caja;
        public ICajaBase Caja {
            get => _caja;
            set => this.Set(nameof(Caja), ref _caja, value);
        }
        private int? _clientId;
        public int? ClientId
        {
            get => _clientId;
            set => this.Set(nameof(ClientId), ref _clientId, value);
        }
        public bool NoClient
        {
            get => !this.ClientId.HasValue;
        }
        public ObservableCollection<ProductoPlazoOpciones> Productos { get; set; }
        public ICollectionView ProductosView { get; set; }
        public ObservableCollection<Models.PlanPagoItem> PlanPago { get; set; }
        private bool _hasPromocionPlazos;
        public bool HasPromocionPlazos
        {
            get => _hasPromocionPlazos;
            set => this.Set(nameof(HasPromocionPlazos), ref _hasPromocionPlazos, value);
        }
        public virtual bool HasSelectedPromocion { get; }
        protected bool SkipPropertyChanged { get; set; }
        private string _devsucursal;
        public string DevSucursal
        {
            get => _devsucursal;
            set => this.Set(nameof(DevSucursal), ref _devsucursal, value);
        }
        private string _devfolio;
        public string DevFolio
        {
            get {return _devfolio;}
            set {
                this.Set(nameof(DevFolio), ref _devfolio, value); 
            }
        }
    }
}

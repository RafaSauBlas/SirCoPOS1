using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using SirCoPOS.Common.Constants;
using SirCoPOS.Common.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SirCoPOS.Client.ViewModels.Caja
{
    class PagoValeViewModel : Helpers.PagoViewModel, IValidatableObject
    {
        public override string Title => "Pago Vale";        
        public override FormaPago FormaPago => FormaPago.VA;
        private Common.ServiceContracts.IDataServiceAsync _proxy;
        private Utilities.Interfaces.IImageView _image;

        public PagoValeViewModel()
        {            
            this.Productos = new ObservableCollection<Utilities.Models.ProductoPlazoOpciones>();
            this.ProductosView = CollectionViewSource.GetDefaultView(this.Productos);
            this.ProductosView.Filter = i =>
            {
                var item = (Utilities.Models.ProductoPlazoOpciones)i;
                if (item.Item.FormasPago.Where(k => k.FormaPago == this.FormaPago && item.Item.Precio > 1).Any())
                    return true;                            
                return false;
            };
            this.Productos.CollectionChanged += Productos_CollectionChanged;
            this.PlanPago = new ObservableCollection<Models.PlanPagoItem>();
            if (!IsInDesignMode)
                _proxy = CommonServiceLocator.ServiceLocator.Current.GetInstance<Common.ServiceContracts.IDataServiceAsync>();
            this.PropertyChanged += PagoValeViewModel_PropertyChanged;
            this.OpenFirma = new RelayCommand<Models.FirmaIndex>(p =>
            {
                var ic = new Converters.ImageUrlMultiConverter();
                var res = ic.Convert(new object[] { p.Id, p.Index }, typeof(string), "FirmaUrl", System.Globalization.CultureInfo.CurrentUICulture);
                //MessageBox.Show($"{p.Id}x{p.Index}");
                _image.OpenImage(res, Vale);                
                
            });
            this.SearchCommand = new GalaSoft.MvvmLight.Command.RelayCommand(async () =>
            {
                valeOK = false;
                //Views.Caja.PagoValeView2 PV2 = new Views.Caja.PagoValeView2();
                try { 
                    this.Vale = await _proxy.FindValeAsync(this.Search);
                }
                catch
                {
                    MessageBox.Show("Ocurrio un Error al buscar el Vale", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                var pago = (Models.Pagos.PagoVale)this.PagoIem;
                pago.Info.Electronica = true;
                if (this.Vale != null)
                {
                    this.valeOK = true;

                    if (!this.Vale.Usado) { 
                        pago.Info.Electronica = this.Vale.Distribuidor.Electronica;                    
                        this.Limite = this.Vale.Limite;
                        this.Search = null;
                        if (!this.HasPromocion)
                            this.SelectedPromocion = this.Promocion.Promociones.FirstOrDefault();
                        if (this.Vale.Distribuidor.Firmas != null && this.Vale.Distribuidor.Firmas.Any())
                        {                        
                            this.SelectedFirma = this.Vale.Distribuidor.Firmas.First();
                        }
                        else
                            this.SelectedFirma = null;
                        this.Caja.UpdatePagos();
                        if (!Vale.Distribuidor.Electronica && this.TotalElectronica > 0)
                        {
                            MessageBox.Show("El vale " + Vale.Vale + " no permite compras de Electrónica" , "Pago Vale", MessageBoxButton.OK, MessageBoxImage.Information);
                            this.valeOK = false;
                        }
                        maxPlazosDist = this.Vale.Distribuidor.maxPlazos;
                        if (Vale.Distribuidor.Electronica && this.maxPlazosDist !=null )
                        {
                            foreach (var e in this.Productos)
                            {
                                if (e.GetPlazos() > this.maxPlazosDist)
                                {
                                    e.SetPlazos((int)this.maxPlazosDist);
                                }
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("El vale " + Vale.Vale + " ya ha sido utilizado con la\nnota de Venta " + Vale.SucursalUsado+"-"+Vale.NotaUsado+ " el " + Vale.FechaUsado.ToString("dd-MMM-yyyy"), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        this.Search = null;
                        this.valeOK = false;
                    }

                }
                else
                {
                    MessageBox.Show("Vale no encontrado, por favor validelo nuevamente.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    this.valeOK = false;
                }
                if (this.valeOK)
                    Messenger.Default.Send<string>("focus", "NextFocus");

            }, () => !String.IsNullOrEmpty(this.Search));

            if (!this.IsInDesignMode)
            {
                _image = CommonServiceLocator.ServiceLocator.Current.GetInstance<Utilities.Interfaces.IImageView>();
                var settings = CommonServiceLocator.ServiceLocator.Current.GetInstance<Utilities.Models.Settings>();
                this.Promocion = _proxy.FindPromocionesVale(settings.Sucursal.Clave, "DISTRIBUIDOR");
                //if (promocion != null)
                //{
                //    //this.Plazos = promocion.Plazos;
                //    //this.SelectedPlazo = promocion.Selected;
                //    this.PagosMax = promocion.PagosMax;
                //    this.Promociones = promocion.Promociones;
                //    this.Fechas = promocion.Fechas;
                //    //this.SelectedPromocion = promocion.Promociones.Any() ? promocion.Promociones.Last() : (DateTime?)null;
                //}
            }

            if (this.IsInDesignMode)
            {
                this.HasPromocionPlazos = true;
                this.Plazos = new int[] { 1, 2, 3 };
                this.SelectedPlazo = 3;
                this.Promocion = new PromocionesValeResponse
                {
                    Promociones = new DateTime[] {
                        DateTime.Parse("2019-01-01"),
                        DateTime.Parse("2019-02-10"),
                        DateTime.Parse("2019-03-20")
                    },
                    Fechas = new DateTime[] {
                        DateTime.Parse("2020-01-01"),
                        DateTime.Parse("2020-02-10"),
                        DateTime.Parse("2020-03-20")
                    },
                    Blindaje = 100
                };
                this.SelectedPromocion = this.Promocion.Promociones.Last();
                this.Primero = 123;
                this.Ultimo = 456;

                //this.Total = 1234.5m;
                this.Pagar = 200m;
                this.Search = "123";
                this.Limite = 1000;
                this.GenerateContraVale = true;
                this.Vale = new Common.Entities.ValeResponse
                {
                    Cancelado = true,
                    CanceladoMotivo = "motivo",
                    Disponible = 199m,
                    Vale = "123",
                    ClienteId = 1,
                    Distribuidor = new Common.Entities.Distribuidor
                    {
                        Id = 1,
                        Cuenta = "0123",
                        Nombre = "nombre",
                        ApPaterno = "appaterno",
                        ApMaterno = "apmaterno",
                        Status = Common.Constants.StatusDistribuidor.SOBREGIRADO,
                        Electronica = true,
                        Promocion = true,
                        ContraVale = true,
                        Firmas = new short[] { 1, 2, 3 }
                    }
                };


                this.Productos.Add(new Utilities.Models.ProductoPlazoOpciones(new Models.Producto { MaxPlazos = 30, Id = 1, Serie = "001", Marca = "a", Modelo = "b", Talla = "c", Precio = 100, DescuentoDirecto = null, HasImage = true, Electronica = true }));
                this.Productos.Add(new Utilities.Models.ProductoPlazoOpciones(new Models.Producto { MaxPlazos = 20, Id = 3, Serie = "003", Marca = "a", Modelo = "b", Talla = "c", Precio = 100, DescuentoDirecto = 10 }));

                this.PlanPago.Add(new Models.PlanPagoItem { Number = 1, Date = DateTime.Parse("2019-01-01"), Amount = 110.99m });
                this.PlanPago.Add(new Models.PlanPagoItem { Number = 2, Date = DateTime.Parse("2019-01-15"), Amount = 100.99m });
                this.PlanPago.Add(new Models.PlanPagoItem { Number = 3, Date = DateTime.Parse("2019-02-01"), Amount = 90m });
            }
        }

        private void Productos_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (var i in e.NewItems)
                {
                    var item = (Utilities.Models.ProductoPlazoOpciones)i;
                    item.PropertyChanged += Item_PropertyChanged;
                }
            }
            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (var i in e.OldItems)
                {
                    var item = (Utilities.Models.ProductoPlazoOpciones)i;
                    item.PropertyChanged -= Item_PropertyChanged;
                }
            }
        }

        private void Item_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(Utilities.Models.ProductoPlazoOpciones.SelectedPlazo):
                    this.UpdatePagos();
                    this.AcceptCommand.RaiseCanExecuteChanged();
                    break;
            }
        }
        
        private void PagoValeViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (this.SkipPropertyChanged)
                return;

            switch (e.PropertyName)
            {
                case nameof(this.Vale):
                    this.RaisePropertyChanged(nameof(this.MayorDisponible));
                    this.RaisePropertyChanged(nameof(this.HasPromocion));
                    this.AcceptCommand.RaiseCanExecuteChanged();
                    if (!this.Vale?.Distribuidor?.ContraVale ?? false)
                        this.GenerateContraVale = false;
                    break;
                case nameof(this.GenerateContraVale):
                case nameof(this.Limite):
                    this.RaisePropertyChanged(nameof(this.ContraVale));
                    this.AcceptCommand.RaiseCanExecuteChanged();
                    break;
                case nameof(this.Pagar):
                    this.UpdatePagos();
                    this.RaisePropertyChanged(nameof(this.ContraVale));
                    this.RaisePropertyChanged(nameof(this.MayorDisponible));
                    break;
                case nameof(this.SelectedPlazo):
                    this.UpdatePagos();
                    this.AcceptCommand.RaiseCanExecuteChanged();
                    break;
                case nameof(this.SelectedPromocion):
                    if (this.SelectedPromocion.HasValue)
                    {
                        var ind = this.Promocion.Promociones.ToList().IndexOf(this.SelectedPromocion.Value);
                        var max = this.Promocion.PagosMax - ind;
                        var min = this.Promocion.PagosMin;
                        if (max > 0)
                        {
                            if (max < min)
                                max = min;
                            var options = Enumerable.Range(1, max);
                            this.Plazos = options;
                        }
                    }
                    this.UpdatePagos();
                    this.AcceptCommand.RaiseCanExecuteChanged();
                    break;
            }
        }
        private void UpdatePagos()
        {
            if (this.CanAccept())
            {
                //var part = this.Pagar.Value / this.SelectedPlazo.Value;
                //this.Primero = Math.Ceiling(part);
                //var res = this.Primero.Value * (this.SelectedPlazo.Value - 1);
                //this.Ultimo = this.Pagar.Value - res;

                //this.PlanPago.Clear();
                //var fec = this.Promocion.Fechas.Where(i => i >= this.SelectedPromocion.Value).ToArray();
                //for (int i = 0; i < this.SelectedPlazo.Value; i++)
                //{
                //    var pp = new Models.PlanPagoItem {
                //        Number = i + 1,
                //        Date = fec[i]
                //    };
                //    if (i == 0)
                //    {
                //        pp.Amount = this.Primero.Value + this.Promocion.Blindaje;
                //    }
                //    else if ((i + 1) == this.SelectedPlazo.Value)
                //    {
                //        pp.Amount = this.Ultimo.Value;
                //    }
                //    else
                //    {
                //        pp.Amount = this.Primero.Value;
                //    }
                //    this.PlanPago.Add(pp);
                //}
                this.PlanPago.Clear();
                this.Primero = null;
                this.Ultimo = null;

                var request = new Common.Entities.Pago
                {
                    //FormaPago = FormaPago.VA,
                    FormaPago = this.FormaPago,
                    Importe = this.Pagar.Value,
                    //Vale = i.Vale,
                    Plazos = this.SelectedPlazo,
                    FechaAplicar = this.SelectedPromocion,
                    //ContraVale = i.ContraVale,
                    //Limite = i.Limite,                    
                    //Negocio = i.Negocio,
                    //NoCuenta = i.NoCuenta
                };
                if (this.Vale.Distribuidor.Electronica)
                {
                    request.ProductosPlazos = this.Productos
                            .Where(i => i.Item.FormasPago.Where(k => k.FormaPago == this.FormaPago).Any() && i.Item.Precio > (decimal)0.01)
                            .Select(i => new ProductoPlazo
                            {
                                Plazos = i.SelectedPlazo,
                                //Importe = i.Item.Precio
                                Serie = i.Item.Serie,
                                Importe = i.Item.FormasPago.Where(k => k.FormaPago == this.FormaPago)
                                    .Single().Importe
                            });
                }
                this.IsBusy = true;
                var detalle = _proxy.GenerarPlanPagosFechas(this.Vale.Distribuidor.Id, request);
                int days;
                if (request.FormaPago == FormaPago.CP)
                {
                    days = -1;
                }
                else { days = -2; }

                var count = 0;
                foreach (var item in detalle)
                {
                    this.PlanPago.Add(new Models.PlanPagoItem
                    {
                        Number = ++count, 
                        Date = item.Key.AddDays(days), 
                        Amount = item.Value
                    });
                }
                this.IsBusy = false;
            }
            else
            {
                this.PlanPago.Clear();
                this.Primero = null;
                this.Ultimo = null;
            }

            this.ProductosView.Refresh();
        }
        protected async override Task<Utilities.Messages.Pago> Prepare(bool acept)
        {
            Utilities.Messages.Pago msg = null;
            if (acept)
            {
                msg = new Utilities.Messages.Pago
                {
                    FormaPago = this.FormaPago,
                    Importe = this.Pagar.Value,
                    Vale = this.Vale.Vale,
                    Cliente = this.Vale.ClienteId,
                    Plazos = this.Plazos,
                    SelectedPlazo = this.SelectedPlazo,
                    Promociones = this.Promocion.Promociones,
                    SelectedPromocion = this.SelectedPromocion,
                    ContraVale = this.GenerateContraVale,
                    Limite = this.Limite,
                    PlazosProductos = this.Productos
                        .Where(i => i.SelectedPlazo.HasValue
                            && i.Item.FormasPago.Where(k => k.FormaPago == this.FormaPago).Any())
                        .Select(i => new ProductoPlazo
                        {
                            Serie = i.Item.Serie,
                            Plazos = i.SelectedPlazo,
                        //Importe = i.Item.Precio //i.Pago - se limpia el pago antes de enviar, por eso queda null el pago
                        Importe = i.Item.FormasPago.Where(k => k.FormaPago == this.FormaPago)
                                        .Single().Importe
                        }).ToArray()
                };
            }
            var res = await base.Prepare(acept);

            return msg;
        }
        protected override void Accept(Utilities.Messages.Pago msg)
        {
            //if (this.Vale.Distribuidor.Electronica)
            //{ 

            //}
            Messenger.Default.Send(msg, this.GID);
        }
        protected override bool CanAccept()
        {
            if (this.Vale != null)
            {
                if (this.Vale.Cancelado)
                    return false;

                this.SkipPropertyChanged = true;
                var isvalid = this.IsValid();
                this.SkipPropertyChanged = false;
                if (!isvalid)
                    return false;

                if (this.Pagar > this.Vale.Disponible)
                    return false;

                if (this.Limite.HasValue)
                {
                    if (this.Pagar > this.Limite)
                        return false;
                    if (this.Limite > this.Vale.Distribuidor.LimiteVale)
                        return false;
                }

                if (this.Vale.Distribuidor.Status != Common.Constants.StatusDistribuidor.ACTIVO)
                    return false;

                if (this.Vale.Distribuidor.Promocion)
                {
                    if (!this.SelectedPromocion.HasValue)
                        return false;                    
                }
                if (this.HasPromocionPlazos)
                {
                    if (!this.SelectedPlazo.HasValue)
                        return false;
                }

                if (this.Productos.Any() && this.Vale.Distribuidor.Electronica)
                {
                    var q = this.ProductosView.Cast<Utilities.Models.ProductoPlazoOpciones>()
                        .Where(i => !i.SelectedPlazo.HasValue);
                    if (q.Any())
                        return false;
                }

                if (this.Pagar > this.TotalCalzado && !this.Vale.Distribuidor.Electronica
                    && this.Productos.Any())
                {
                    return false;
                }
                if (this.Pagar > this.Total)
                {
                    return false;
                }

                return true;
            }
            return false;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.Vale != null && this.Pagar.HasValue)
            {
                if (this.Pagar > this.Vale.Disponible)
                    yield return new ValidationResult("fondos insuficientes", new string[] { "Pagar" });
            }
        }

        private int? _SelectedFirma;
        public int? SelectedFirma
        {
            get { return _SelectedFirma; }
            set { Set(nameof(this.SelectedFirma), ref _SelectedFirma, value); }
        }

        private bool _valeok;
        public bool valeOK
        {
            get { return _valeok; }
            set { Set(nameof(this.valeOK), ref _valeok, value); }
        }
        public override bool HasSelectedPromocion {
            get {
                if (this.SelectedPromocion.HasValue
                    && this.Promocion != null
                    && this.Promocion.Promociones.Any())
                {
                    return this.SelectedPromocion != this.Promocion.Promociones.First();
                }
                return false;
            }
        }

        #region computed
        public bool MayorDisponible
        {
            get {
                if (this.Vale != null && this.Pagar.HasValue)
                {
                    return this.Pagar > this.Vale.Disponible;
                }
                return false;
            }
        }
        public bool HasPromocion
        { 
            get {
                if (this.Vale == null)
                    return false;

                if (!this.Vale.Distribuidor.Promocion)
                    return false;

                if (!this.HasPromocionPlazos)
                    return false;

                return true;
            }
        }
        #endregion
        #region properties   
        
        private string _search;
        public string Search {
            get { return _search; }
            set {
                this.Set(nameof(this.Search), ref _search, value);
            }
        }
        private Common.Entities.ValeResponse _vale;
        public Common.Entities.ValeResponse Vale
        {
            get { return _vale; }
            set { this.Set(nameof(this.Vale), ref _vale, value); }
        }


        //computed
        private decimal? _primero;
        public decimal? Primero
        {
            get { return _primero; }
            set
            {
                this.Set(nameof(this.Primero), ref _primero, value);
            }
        }
        //computed
        private decimal? _ultimo;
        public decimal? Ultimo
        {
            get { return _ultimo; }
            set
            {
                this.Set(nameof(this.Ultimo), ref _ultimo, value);
            }
        }
        private PromocionesValeResponse _promocion;
        public PromocionesValeResponse Promocion
        {
            get { return _promocion; }
            set
            {
                this.Set(nameof(this.Promocion), ref _promocion, value);
            }
        }
        
        private DateTime? _selectedPromocion;
        public DateTime? SelectedPromocion
        {
            get { return _selectedPromocion; }
            set { this.Set(nameof(this.SelectedPromocion), ref _selectedPromocion, value); }
        }

        private IEnumerable<int> _plazos;
        public IEnumerable<int> Plazos
        {
            get { return _plazos; }
            set
            {
                this.Set(nameof(this.Plazos), ref _plazos, value);
            }
        }

        private int? _selectedPlazo;
        public int? SelectedPlazo
        {
            get { return _selectedPlazo; }
            set
            {
                this.Set(nameof(this.SelectedPlazo), ref _selectedPlazo, value);
            }
        }
        private decimal? _limite;
        public decimal? Limite
        {
            get => _limite;
            set => this.Set(nameof(Limite), ref _limite, value);
        }
        public decimal? ContraVale
        {
            get {
                if (this.GenerateContraVale && this.Limite.HasValue
                    && this.Pagar.HasValue)
                {
                    var dif = this.Limite - this.Pagar;
                    return dif < 0 ? 0 : dif;
                }
                return null;
            }
        }
        private bool _generateContraVale;
        public bool GenerateContraVale
        {
            get => _generateContraVale;
            set => this.Set(nameof(GenerateContraVale), ref _generateContraVale, value);
        }
        #endregion
        #region commands
        public GalaSoft.MvvmLight.Command.RelayCommand SearchCommand { get; protected set; }
        public RelayCommand<Models.FirmaIndex> OpenFirma { get; private set; }
        #endregion
    }
}

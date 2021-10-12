using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using SirCoPOS.Common.Constants;

namespace SirCoPOS.Client.ViewModels.Caja
{
    public class PagoContraValeViewModel : Helpers.PagoViewModel
    {
        public override string Title => "Pago Contra Vale";
        private Common.ServiceContracts.IDataServiceAsync _proxy;
        private Helpers.CommonHelper _common;
        public PagoContraValeViewModel()
        {
            _common = new Helpers.CommonHelper();
            if (!IsInDesignMode)
                _proxy = CommonServiceLocator.ServiceLocator.Current.GetInstance<Common.ServiceContracts.IDataServiceAsync>();
            this.PropertyChanged += PagoValeViewModel_PropertyChanged;
            this.SearchCommand = new GalaSoft.MvvmLight.Command.RelayCommand(async () =>
            {
                this.IsBusy = true;
                var cv = _common.PrepareContraVale(this.Search);
                this.Vale = await _proxy.FindContraValeAsync(this.SucursalSearch, cv);
                if (this.Vale != null)
                {
                    if (this.Vale.Cancelado)
                    {
                        MessageBox.Show("ContraVale Cancelado", "Pago ContraVale", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    { 
                        this.Search = null;
                        if (!this.Vale.Distribuidor.Promocion)
                            this.SelectedPromocion = this.Promociones.FirstOrDefault();
                        }
                }
                else
                {
                    MessageBox.Show("not found");
                }
                this.IsBusy = false;
            }, () => !String.IsNullOrEmpty(this.Search) && !String.IsNullOrEmpty(this.SucursalSearch));

            if (!this.IsInDesignMode)
            {
                var settings = CommonServiceLocator.ServiceLocator.Current.GetInstance<Utilities.Models.Settings>();
                var promocion = _proxy.FindPromocionesVale(settings.Sucursal.Clave, "DISTRIBUIDOR");
                if (promocion != null)
                {
                    //this.Plazos = promocion.Plazos;
                    //this.SelectedPlazo = promocion.Selected;
                    this.PagosMax = promocion.PagosMax;
                    this.Promociones = promocion.Promociones;
                    //this.SelectedPromocion = promocion.Promociones.Any() ? promocion.Promociones.Last() : (DateTime?)null;
                }
            }

            if (this.IsInDesignMode)
            {
                this.Plazos = new int[] { 1, 2, 3 };
                this.SelectedPlazo = 3;
                this.PagosMax = 10;
                this.Promociones = new DateTime[] {
                        DateTime.Parse("2019-01-01"),
                        DateTime.Parse("2019-02-10"),
                        DateTime.Parse("2019-03-20")
                    };
                this.SelectedPromocion = this.Promociones.Last();
                this.Primero = 123;
                this.Ultimo = 456;

                //this.Total = 1234.5m;
                this.Pagar = 100m;
                this.Search = "123";
                this.Limite = 1000;
                this.GenerateContraVale = true;
                this.Vale = new Common.Entities.CValeResponse
                {
                    Cancelado = false,
                    Disponible = 199m,
                    Vigencia = DateTime.Parse("2020-01-15"),
                    Vale = "123",
                    ClienteId = 1,
                    Distribuidor = new Common.Entities.Distribuidor
                    {
                        Id = 1,
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
            }
        }
        public override FormaPago FormaPago => FormaPago.CV;
        private void PagoValeViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(this.Vale):
                    this.RaisePropertyChanged(nameof(this.Expirado));
                    this.AcceptCommand.RaiseCanExecuteChanged();
                    if (!this.Vale?.Distribuidor?.ContraVale ?? false)
                        this.GenerateContraVale = false;
                    break;
                case nameof(this.GenerateContraVale):
                case nameof(this.Limite):
                    this.RaisePropertyChanged(nameof(this.ContraVale));
                    break;
                case nameof(this.Pagar):
                    this.UpdatePagos();
                    this.RaisePropertyChanged(nameof(this.ContraVale));
                    break;
                case nameof(this.SelectedPlazo):
                    this.UpdatePagos();
                    this.AcceptCommand.RaiseCanExecuteChanged();
                    break;
                case nameof(this.SelectedPromocion):
                    if (this.SelectedPromocion.HasValue)
                    {
                        var ind = this.Promociones.ToList().IndexOf(this.SelectedPromocion.Value);
                        var max = this.PagosMax - ind;
                        var options = Enumerable.Range(1, max);
                        this.Plazos = options;
                    }
                    this.AcceptCommand.RaiseCanExecuteChanged();
                    break;
            }
        }
        private void UpdatePagos()
        {
            if (this.SelectedPlazo.HasValue && this.Pagar.HasValue)
            {
                var part = this.Pagar.Value / this.SelectedPlazo.Value;
                this.Primero = Math.Ceiling(part);
                var res = this.Primero.Value * (this.SelectedPlazo.Value - 1);
                this.Ultimo = this.Pagar.Value - res;
            }
            else
            {
                this.Primero = null;
                this.Ultimo = null;
            }
        }
        protected override void Accept(Utilities.Messages.Pago p)
        {
            Messenger.Default.Send(
                    new Utilities.Messages.Pago
                    {
                        FormaPago = FormaPago.CV,
                        Importe = this.Pagar.Value,
                        Vale = this.Vale.Vale,
                        Cliente = this.Vale.ClienteId,
                        Plazos = this.Plazos,
                        SelectedPlazo = this.SelectedPlazo,
                        Promociones = this.Promociones,
                        SelectedPromocion = this.SelectedPromocion,
                        ContraVale = this.GenerateContraVale,
                        Sucursal = this.Vale.Sucursal
                    }, this.GID);
        }
        protected override bool CanAccept()
        {
            if (this.Vale != null)
            {
                if (this.Expirado)
                    return false;

                if (this.Pagar > this.Vale.Disponible)
                    return false;

                if (this.Vale.Distribuidor.Status != Common.Constants.StatusDistribuidor.ACTIVO)
                    return false;

                if (this.Vale.Distribuidor.Promocion)
                {
                    if (!this.SelectedPlazo.HasValue)
                        return false;
                    if (!this.SelectedPromocion.HasValue)
                        return false;
                }

                return true;
            }
            return false;
        }
        #region properties                
        private string _search;
        public string Search
        {
            get { return _search; }
            set
            {
                this.Set(nameof(this.Search), ref _search, value);
            }
        }
        private string _sucursalSearch;
        public string SucursalSearch
        {
            get { return _sucursalSearch; }
            set
            {
                this.Set(nameof(this.SucursalSearch), ref _sucursalSearch, value);
            }
        }
        private Common.Entities.CValeResponse _vale;
        public Common.Entities.CValeResponse Vale
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
        private IEnumerable<DateTime> _promociones;
        public IEnumerable<DateTime> Promociones
        {
            get { return _promociones; }
            set
            {
                this.Set(nameof(this.Promociones), ref _promociones, value);
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

        public int PagosMax { get; private set; }

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
            get
            {
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
        #region computed
        public bool Expirado
        {
            get
            {
                if (this.Vale != null)
                {
                    if (this.Vale.Vigencia < DateTime.Now)
                        return true;
                }
                return false;
            }
        }
        #endregion
        #region commands
        public GalaSoft.MvvmLight.Command.RelayCommand SearchCommand { get; private set; }
        #endregion
    }
}

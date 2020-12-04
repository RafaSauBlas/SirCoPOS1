using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using SirCoPOS.Common.Constants;
using SirCoPOS.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SirCoPOS.Client.ViewModels.Caja
{
    class PagoCreditoViewModel : Helpers.PagoViewModel
    {
        public override string Title => "Pago Credito Personal";
        public override FormaPago FormaPago => FormaPago.CP;
        protected Common.ServiceContracts.IDataServiceAsync _proxy;
        private Helpers.CommonHelper _common;
        protected async virtual Task<Distribuidor> Find(string id)
        {
            var res = _common.PrepareTarjetahabiente(id);
            //return await _proxy.FindTarjetahabienteAsync(res);
            throw new NotImplementedException();
        }
        public PagoCreditoViewModel()
        {
            if (!IsInDesignMode)
            {
                _common = new Helpers.CommonHelper();
                _proxy = CommonServiceLocator.ServiceLocator.Current.GetInstance<Common.ServiceContracts.IDataServiceAsync>();
            }
            this.PropertyChanged += PagoValeViewModel_PropertyChanged;
            this.SearchCommand = new GalaSoft.MvvmLight.Command.RelayCommand(async () =>
            {
                this.IsBusy = true;
                this.Tarjetahabiente = await this.Find(this.Search);
                if (this.Tarjetahabiente != null)
                {
                    this.Search = null;
                    if (!this.Tarjetahabiente.Promocion)
                        this.SelectedPromocion = this.Promociones.FirstOrDefault();
                }
                else
                {
                    MessageBox.Show("not found");
                }
                this.IsBusy = false;
            }, () => !String.IsNullOrEmpty(this.Search));

            if (!this.IsInDesignMode)
            {
                var settings = CommonServiceLocator.ServiceLocator.Current.GetInstance<Utilities.Models.Settings>();
                var promocion = _proxy.FindPromocionesVale(settings.Sucursal.Clave);
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
                this.Tarjetahabiente = new Common.Entities.Distribuidor
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
                };
            }
        }
        
        private void PagoValeViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(this.Tarjetahabiente):
                    this.AcceptCommand.RaiseCanExecuteChanged();                    
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
                    break;
                case nameof(this.SelectedPromocion):
                    if (this.SelectedPromocion.HasValue)
                    {
                        var ind = this.Promociones.ToList().IndexOf(this.SelectedPromocion.Value);
                        var max = this.PagosMax - ind;
                        var options = Enumerable.Range(1, max);
                        this.Plazos = options;
                    }
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
                        FormaPago = this.FormaPago,
                        Importe = this.Pagar.Value,
                        Vale = this.Tarjetahabiente.Number,
                        //Vale = this.Tarjetahabiente.Vale,
                        Cliente = this.Tarjetahabiente.ClienteId,
                        Plazos = this.Plazos,
                        SelectedPlazo = this.SelectedPlazo,
                        Promociones = this.Promociones,
                        SelectedPromocion = this.SelectedPromocion,
                        DistribuidorId = this.Tarjetahabiente.Id
                    }, this.GID);
        }
        protected override bool CanAccept()
        {
            if (this.Tarjetahabiente != null)
            {
                if (this.Pagar > this.Tarjetahabiente.Disponible)
                    return false;

                if (this.Limite.HasValue)
                {
                    if (this.Pagar > this.Limite)
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
        private Common.Entities.Distribuidor _th;
        public Common.Entities.Distribuidor Tarjetahabiente
        {
            get { return _th; }
            set { this.Set(nameof(this.Tarjetahabiente), ref _th, value); }
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
        #region commands
        public GalaSoft.MvvmLight.Command.RelayCommand SearchCommand { get; private set; }
        #endregion
    }
}

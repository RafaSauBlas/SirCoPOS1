using GalaSoft.MvvmLight.Messaging;
using SirCoPOS.Common.Constants;
using SirCoPOS.Common.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SirCoPOS.Client.ViewModels.Caja
{
    class PagoValeExternoViewModel : Helpers.PagoViewModel
    {
        public override string Title => "Pago Vale Externo";
        private Common.ServiceContracts.IDataServiceAsync _proxy;
        public PagoValeExternoViewModel()
        {
            this.PlanPago = new ObservableCollection<Models.PlanPagoItem>();
            this.PropertyChanged += PagoValeExternoViewModel_PropertyChanged;
            if (!IsInDesignMode)
                _proxy = CommonServiceLocator.ServiceLocator.Current.GetInstance<Common.ServiceContracts.IDataServiceAsync>();

            this.SearchCommand = new GalaSoft.MvvmLight.Command.RelayCommand(() => 
            {

                this.Vale = _proxy.FindDistribuidorExterno(this.SelectedNegocio.Value, this.Cuenta, this.ValeSearch);
                if (this.Vale == null)
                    MessageBox.Show("Distribuidor no encontrado.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }, () => !string.IsNullOrEmpty(this.ValeSearch) && this.SelectedNegocio.HasValue && !string.IsNullOrEmpty(this.Cuenta));

            if (!this.IsInDesignMode)
            {
                var settings = CommonServiceLocator.ServiceLocator.Current.GetInstance<Utilities.Models.Settings>();
                this.Promocion = _proxy.FindPromocionesVale(settings.Sucursal.Clave);
                this.Negocios = _proxy.GetNegocios();
            }

            if (this.IsInDesignMode)
            {
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
                this.Pagar = 100m;
                this.ValeSearch = "vale";
                this.Negocios = new Common.Entities.NegocioExterno[] 
                {
                    new Common.Entities.NegocioExterno { Id = 1, Negocio = "a", Descripcion = "n1" },
                    new Common.Entities.NegocioExterno { Id = 2, Negocio = "b", Descripcion = "n2" },
                    new Common.Entities.NegocioExterno { Id = 3, Negocio = "c", Descripcion = "n3" }
                };
                this.Negocio = "a";
                this.SelectedNegocio = 1;
                this.Cuenta = "0123456";
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
            }
        }

        private void PagoValeExternoViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(this.Negocio):
                    if (!String.IsNullOrEmpty(this.Negocio))
                    {
                        var q = this.Negocios.Where(i => i.Negocio.ToLower().StartsWith(this.Negocio.ToLower()))
                            .OrderBy(i => i.Negocio);
                        var count = q.Count();
                        if (count == 1)
                        {
                            this.SelectedNegocio = q.Single().Id;
                        }
                    }
                    break;
                case nameof(this.Vale):
                    this.AcceptCommand.RaiseCanExecuteChanged();
                    break;
                case nameof(this.Pagar):
                    this.UpdatePagos();
                    this.AcceptCommand.RaiseCanExecuteChanged();
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
                        if (max > 0)
                        {
                            var options = Enumerable.Range(1, max);
                            this.Plazos = options;
                        }
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

                this.PlanPago.Clear();
                var fec = this.Promocion.Fechas.Where(i => i >= this.SelectedPromocion.Value).ToArray();
                for (int i = 0; i < this.SelectedPlazo.Value; i++)
                {
                    var pp = new Models.PlanPagoItem
                    {
                        Number = i + 1,
                        Date = fec[i]
                    };
                    if (i == 0)
                    {
                        pp.Amount = this.Primero.Value + this.Promocion.Blindaje;
                    }
                    else if ((i + 1) == this.SelectedPlazo.Value)
                    {
                        pp.Amount = this.Ultimo.Value;
                    }
                    else
                    {
                        pp.Amount = this.Primero.Value;
                    }
                    this.PlanPago.Add(pp);
                }
            }
            else
            {
                this.PlanPago.Clear();
                this.Primero = null;
                this.Ultimo = null;
            }
        }
        public override FormaPago FormaPago => FormaPago.VE;
        protected override bool CanAccept()
        {
            if (this.Vale != null)
            {
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
        protected override void Accept(Utilities.Messages.Pago p)
        {
            var msg = new Utilities.Messages.Pago
            {
                FormaPago = this.FormaPago, 
                DistribuidorId = this.Vale.Distribuidor.Id,
                NoCuenta = this.Cuenta,
                Negocio = this.SelectedNegocio,
                Importe = this.Pagar.Value,
                Vale = this.ValeSearch,
                Cliente = null,
                Plazos = this.Plazos,
                SelectedPlazo = this.SelectedPlazo,
                Promociones = this.Promocion.Promociones,
                SelectedPromocion = this.SelectedPromocion,
                PlazosProductos = this.Productos.Select(i => new ProductoPlazo
                {
                    Serie = i.Item.Serie,
                    Plazos = i.SelectedPlazo,
                    Importe = i.Item.Precio //i.Pago - se limpia el pago antes de enviar, por eso queda null el pago
                }).ToArray()
            };
            Messenger.Default.Send(msg, this.GID);
        }
        #region properties
        private string _valeSearch;
        public string ValeSearch
        {
            get => _valeSearch;
            set => this.Set(nameof(this.ValeSearch), ref _valeSearch, value);
        }
        private int? _selectedNegocio;
        public int? SelectedNegocio
        {
            get => _selectedNegocio;
            set => this.Set(nameof(this.SelectedNegocio), ref _selectedNegocio, value);
        }
        private string _cuenta;
        public string Cuenta
        {
            get => _cuenta;
            set => this.Set(nameof(this.Cuenta), ref _cuenta, value);
        }
        private IEnumerable<Common.Entities.NegocioExterno> _negocios;
        public IEnumerable<Common.Entities.NegocioExterno> Negocios
        {
            get => _negocios;
            set => this.Set(nameof(this.Negocios), ref _negocios, value);
        }
        private Common.Entities.ValeResponse _vale;
        public Common.Entities.ValeResponse Vale
        {
            get { return _vale; }
            set { this.Set(nameof(this.Vale), ref _vale, value); }
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
        private DateTime? _selectedPromocion;
        public DateTime? SelectedPromocion
        {
            get { return _selectedPromocion; }
            set { this.Set(nameof(this.SelectedPromocion), ref _selectedPromocion, value); }
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
        private int? _selectedPlazo;
        public int? SelectedPlazo
        {
            get { return _selectedPlazo; }
            set
            {
                this.Set(nameof(this.SelectedPlazo), ref _selectedPlazo, value);
            }
        }
        private string _Negocio;
        public string Negocio
        {
            get { return _Negocio; }
            set { Set(nameof(this.Negocio), ref _Negocio, value); }
        }

        #endregion
        #region Commands
        public GalaSoft.MvvmLight.Command.RelayCommand SearchCommand { get; private set; }
        #endregion
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
    }
}

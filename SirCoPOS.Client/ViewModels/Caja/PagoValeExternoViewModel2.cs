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
    class PagoValeExternoViewModel2 : PagoValeViewModel
    {
        public override string Title => "Pago Vale Externo";
        public override FormaPago FormaPago => FormaPago.VE;
        private Common.ServiceContracts.IDataServiceAsync _proxy;
        public PagoValeExternoViewModel2()
        {
            this.PropertyChanged += PagoValeExternoViewModel2_PropertyChanged;
            if (!this.IsInDesignMode)
            {
                _proxy = CommonServiceLocator.ServiceLocator.Current.GetInstance<Common.ServiceContracts.IDataServiceAsync>();
                this.Negocios = _proxy.GetNegocios();
            }

            this.SearchCommand = new GalaSoft.MvvmLight.Command.RelayCommand(() =>
            {
                this.Vale = _proxy.FindDistribuidorExterno(this.SelectedNegocio.Value, this.Cuenta, this.ValeSearch);
                var pago = (Models.Pagos.PagoVale)this.PagoIem;
                if (this.Vale != null)
                {
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
                }
                else
                    MessageBox.Show("Distribuidor no encontrado.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }, () => !string.IsNullOrEmpty(this.ValeSearch) && this.SelectedNegocio.HasValue && !string.IsNullOrEmpty(this.Cuenta));


            if (this.IsInDesignMode)
            { 
            
            }
        }

        private void PagoValeExternoViewModel2_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
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
            }
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
                PlazosProductos = this.Productos
                        .Where(i => i.SelectedPlazo.HasValue
                            && i.Item.FormasPago.Where(k => k.FormaPago == this.FormaPago).Any())
                        .Select(i => new ProductoPlazo
                        {
                            Serie = i.Item.Serie,
                            Plazos = i.SelectedPlazo,
                            Importe = i.Item.FormasPago.Where(k => k.FormaPago == this.FormaPago).Single().Importe
                        }).ToArray()
            };
            Messenger.Default.Send(msg, this.GID);
        }
        #region properties
        private string _Negocio;
        public string Negocio
        {
            get { return _Negocio; }
            set { Set(nameof(this.Negocio), ref _Negocio, value); }
        }
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
        #endregion
    }
}

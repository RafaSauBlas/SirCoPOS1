using GalaSoft.MvvmLight.Messaging;
using SirCoPOS.Common.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SirCoPOS.Client.ViewModels.Caja
{
    class PagoContraValeViewModel2 : PagoValeViewModel
    {
        public override string Title => "Pago Contra Vale";
        public override FormaPago FormaPago => FormaPago.CV;
        private Common.ServiceContracts.IDataServiceAsync _proxy;
        private Helpers.CommonHelper _common;
        public PagoContraValeViewModel2()
        {
            _common = new Helpers.CommonHelper();
            if (!IsInDesignMode)
                _proxy = CommonServiceLocator.ServiceLocator.Current.GetInstance<Common.ServiceContracts.IDataServiceAsync>();
            this.PropertyChanged += PagoContraValeViewModel2_PropertyChanged;
            this.SearchCommand = new GalaSoft.MvvmLight.Command.RelayCommand(async () =>
            {
                this.IsBusy = true;
                var cv = _common.PrepareContraVale(this.Search);
                this.Vale = await _proxy.FindContraValeAsync(this.SucursalSearch, cv);
                if (this.Vale != null)
                {
                    this.SucursalSearch = null;
                    this.Search = null;
                    if (!this.Vale.Distribuidor.Promocion)
                        this.SelectedPromocion = this.Promocion.Promociones.FirstOrDefault();
                }
                else
                {
                    MessageBox.Show("not found");
                }
                this.IsBusy = false;
            }, () => !String.IsNullOrEmpty(this.Search) && !String.IsNullOrEmpty(this.SucursalSearch));

        }

        private void PagoContraValeViewModel2_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(this.Vale):
                    this.RaisePropertyChanged(nameof(this.Expirado));
                    break;
            }
        }
        protected override bool CanAccept()
        {
            if (base.CanAccept())
            {
                if (this.Expirado)
                    return false;
                return true;
            }
            return false;
        }
        protected override void Accept(Utilities.Messages.Pago p)
        {
            var cvale = (Common.Entities.CValeResponse)this.Vale;
            Messenger.Default.Send(
                    new Utilities.Messages.Pago
                    {
                        FormaPago = FormaPago.CV,
                        Importe = this.Pagar.Value,
                        Vale = this.Vale.Vale,
                        Cliente = this.Vale.ClienteId,
                        Plazos = this.Plazos,
                        SelectedPlazo = this.SelectedPlazo,
                        Promociones = this.Promocion.Promociones,
                        SelectedPromocion = this.SelectedPromocion,
                        ContraVale = this.GenerateContraVale,
                        Sucursal = cvale.Sucursal
                    }, this.GID);
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
    }
}

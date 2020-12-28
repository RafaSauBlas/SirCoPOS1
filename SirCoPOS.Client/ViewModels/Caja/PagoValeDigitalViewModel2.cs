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
    class PagoValeDigitalViewModel2 : PagoValeViewModel
    {
        public override string Title => "Pago Vale Digital";
        private Common.ServiceContracts.IDataServiceAsync _proxy;
        public override FormaPago FormaPago => FormaPago.VD;
        public PagoValeDigitalViewModel2()
        {
            if (!this.IsInDesignMode)
            {                
                _proxy = CommonServiceLocator.ServiceLocator.Current.GetInstance<Common.ServiceContracts.IDataServiceAsync>();
            }
            this.PropertyChanged += PagoValeDigitalViewModel2_PropertyChanged;
            this.SearchCommand = new GalaSoft.MvvmLight.Command.RelayCommand(async () =>
            {
                this.IsBusy = true;
                this.Vale = await _proxy.FindValeDigitalAsync(this.Search);
                if (this.Vale != null)
                {
                    this.Search = null;
                    if (!this.Vale.Distribuidor.Promocion)
                        this.SelectedPromocion = this.Promocion.Promociones.FirstOrDefault();
                }
                else
                {
                    MessageBox.Show("Not found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                this.IsBusy = false;
            }, () => !String.IsNullOrEmpty(this.Search));
        }

        private void PagoValeDigitalViewModel2_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(this.ClientId):
                    {
                        this.Vale = _proxy.FindValeDigitalByClient(this.ClientId.Value);
                        if (this.Vale != null)
                        {
                            this.Search = null;
                            if (!this.Vale.Distribuidor.Promocion)
                                this.SelectedPromocion = this.Promocion.Promociones.FirstOrDefault();
                        }
                    }
                    break;
            }
        }

        protected override void Accept(Utilities.Messages.Pago p)
        {
            Messenger.Default.Send(
                    new Utilities.Messages.Pago
                    {
                        FormaPago = this.FormaPago,
                        Importe = this.Pagar.Value,
                        Vale = this.Vale.Vale,
                        Cliente = this.Vale.ClienteId,
                        Plazos = this.Plazos,
                        SelectedPlazo = this.SelectedPlazo,
                        Promociones = this.Promocion.Promociones,
                        SelectedPromocion = this.SelectedPromocion
                    }, this.GID);
        }
    }
}

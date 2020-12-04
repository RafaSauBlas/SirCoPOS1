using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using GalaSoft.MvvmLight.Messaging;
using SirCoPOS.Common.Constants;

namespace SirCoPOS.Client.ViewModels.Caja
{
    class PagoMonederoElectronicoViewModel : Helpers.PagoViewModel
    {
        public override string Title => "Pago Monedero Electronico";
        private Common.ServiceContracts.IDataServiceAsync _proxy;
        
        public PagoMonederoElectronicoViewModel()
        {
            if (!IsInDesignMode)
                _proxy = CommonServiceLocator.ServiceLocator.Current.GetInstance<Common.ServiceContracts.IDataServiceAsync>();
            this.PropertyChanged += PagoMonederoElectronicoViewModel_PropertyChanged;
        }

        private async void PagoMonederoElectronicoViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(this.Disponible):
                    this.AcceptCommand.RaiseCanExecuteChanged();
                    break;
                case nameof(this.ClientId):
                    {
                        this.Disponible = await _proxy.FindMonederoAsync(this.ClientId.Value);                        
                    }
                    break;
            }
        }

        public override FormaPago FormaPago => FormaPago.MD;

        protected override void Accept(Utilities.Messages.Pago p)
        {
            Messenger.Default.Send(
                    new Utilities.Messages.Pago
                    {
                        FormaPago = this.FormaPago,
                        Importe = this.Pagar.Value,
                        Cliente = this.ClientId,                        
                    }, this.GID);
        }

        protected override bool CanAccept()
        {
            if (this.Disponible.HasValue)
            {
                if (this.Pagar > this.Disponible)
                    return false;
                
                return true;
            }
            return false;
        }

        private decimal? _disponible;
        public decimal? Disponible
        {
            get => _disponible;
            set => this.Set(nameof(this.Disponible), ref _disponible, value);
        }
    }
}

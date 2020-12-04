using SirCoPOS.Common.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Client.ViewModels.Caja
{
    class PagoTarjetaCreditoViewModel : Helpers.PagoViewModel
    {
        public override string Title => "Pago Tarjeta Crédito";
        public PagoTarjetaCreditoViewModel()
        {
            this.PropertyChanged += PagoTarjetaCreditoViewModel_PropertyChanged;
            
            if (this.IsInDesignMode)
            {
                //this.Total = 900;
                this.Pagar = 350;
                this.Terminacion = "123";
                this.Referencia = "112233";
            }
        }
        public override FormaPago FormaPago => FormaPago.TC;
        protected override void Accept(Utilities.Messages.Pago p)
        {
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(
                    new Utilities.Messages.Pago
                    {
                        FormaPago = this.FormaPago,
                        Importe = this.Pagar.Value,
                        Terminacion = this.Terminacion,
                        Referencia = this.Referencia
                    }, this.GID);
        }
        protected override bool CanAccept()
        {
            if (!this.IsValid())
                return false;

            if (!String.IsNullOrEmpty(this.Terminacion) && !String.IsNullOrEmpty(this.Referencia) && (this.Pagar ?? 0) > 0)
            {
                var q = this.Caja.Pagos.Where(i => i.FormaPago == this.FormaPago && i != this.PagoIem)
                    .OfType<Models.Pagos.PagoTarjeta>()
                    .Where(i => i.Terminacion == this.Terminacion && i.Referencia == this.Referencia);
                if (!q.Any())
                    return true;
            }
            return false;
        }
        private void PagoTarjetaCreditoViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(this.Total):
                case nameof(this.Pagar):
                    this.RaisePropertyChanged(nameof(this.Pendiente));
                    this.AcceptCommand.RaiseCanExecuteChanged();
                    break;
                case nameof(this.Terminacion):
                case nameof(this.Referencia):
                    this.AcceptCommand.RaiseCanExecuteChanged();
                    break;
            }
        }
        #region properties        
        private string _terminacion;
        [Required]
        public string Terminacion
        {
            get { return _terminacion; }
            set { this.Set(nameof(this.Terminacion), ref _terminacion, value); }
        }
        private string _referencia;
        [Required]
        public string Referencia
        {
            get { return _referencia; }
            set { this.Set(nameof(this.Referencia), ref _referencia, value); }
        }
        #endregion
        #region computed        
        #endregion
        #region commands
        
        #endregion
    }
}

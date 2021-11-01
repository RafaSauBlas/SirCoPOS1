using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using SirCoPOS.Common.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SirCoPOS.Client.ViewModels.Caja
{
    class PagoGoViewModel : Helpers.PagoViewModel
    {
        public override string Title => "Pago GoPlazos";
        private readonly Common.ServiceContracts.IDataServiceAsync _data;

        public PagoGoViewModel()
        {
            this.PropertyChanged += PagoGoViewModel_PropertyChanged;
            _data = CommonServiceLocator.ServiceLocator.Current.GetInstance<Common.ServiceContracts.IDataServiceAsync>();

            this.cantMinima = _data.getminPago(Common.Constants.Parametros.MINPAGOGO);
            
            this.CompletarCommand = new RelayCommand(() =>
            {
                this.Pagar = this.Pagar + this.Pendiente;
            });
            if (this.IsInDesignMode)
            {
                //this.Total = 1500;
                this.Pagar = 350;
                this.PagaCon = 500;
            }            
        }
        public override FormaPago FormaPago => FormaPago.GO;
        protected override void Init()
        {
            this.PagaCon = this.Total;
            base.Init();
        }
        protected override bool CanAccept()
        {
            if (!this.IsValid())
                return false;

            return this.Pagar.HasValue && this.Pagar != 0
                && this.Pagar >= this.cantMinima
                && this.PagaCon.HasValue && this.PagaCon != 0
                && this.Pendiente >= 0 && this.PagaCon >= this.Pagar;

        }
        protected override void Accept(Utilities.Messages.Pago p)
        {
            Messenger.Default.Send(
                    new Utilities.Messages.Pago
                    {  
                        FormaPago = FormaPago.GO,
                        Importe = this.Pagar.Value,
                        Efectivo = this.PagaCon.Value
                    }, this.GID);
        }
        private void PagoGoViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(this.Total):
                case nameof(this.Pagar):
                case nameof(this.PagaCon):
                    this.RaisePropertyChanged(nameof(this.Pendiente));
                    this.RaisePropertyChanged(nameof(this.Regresar));
                    this.AcceptCommand.RaiseCanExecuteChanged();
                    break;
            }
        }        

        #region properties
        private decimal _cantminima;
        public decimal cantMinima
        {
            get { return _cantminima; }
            set { this.Set(nameof(this.cantMinima), ref _cantminima, value); }
        }
        private decimal? _pagaCon;
        [Required]
        public decimal? PagaCon {
            get { return _pagaCon; }
            set { this.Set(nameof(this.PagaCon), ref _pagaCon, value); }
        }
        #endregion
        #region computed
        public decimal? Regresar { get { return (this.PagaCon ?? 0) - (this.Pagar ?? 0); } }
        #endregion
        #region commands        
        public RelayCommand CompletarCommand { get; private set; }
        #endregion
    }
}

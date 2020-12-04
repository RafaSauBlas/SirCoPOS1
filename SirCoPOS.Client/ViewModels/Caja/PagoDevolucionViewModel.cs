using GalaSoft.MvvmLight.Command;
using SirCoPOS.Common.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Client.ViewModels.Caja
{
    class PagoDevolucionViewModel : Helpers.PagoViewModel
    {
        public override string Title => "Pago Devolucion";
        private Common.ServiceContracts.IDataServiceAsync _proxy;
        public PagoDevolucionViewModel()
        {
            if (!this.IsInDesignMode)
                _proxy = CommonServiceLocator.ServiceLocator.Current.GetInstance<Common.ServiceContracts.IDataServiceAsync>();

            this.PropertyChanged += PagoDevolucionViewModel_PropertyChanged;

            this.FindCommand = new RelayCommand(async () => {
                this.IsBusy = true;
                this.Devolucion = await _proxy.FindDevolucionAsync(this.Sucursal, this.Folio);
                if (this.Devolucion != null)
                {
                    if (this.Devolucion.Disponible >= this.Total)
                        this.Pagar = this.Total;
                    else if (this.Devolucion.Disponible > 0)
                        this.Pagar = this.Devolucion.Disponible;
                }
                this.IsBusy = false;
            }, () => !String.IsNullOrEmpty(this.Sucursal) && !String.IsNullOrEmpty(this.Folio));

            if (this.IsInDesignMode)
            {
                //this.Total = 123.4m;
                this.Pagar = 456;
                this.Sucursal = "01";
                this.Folio = "123456";
                this.Devolucion = new Common.Entities.Devolucion
                {
                    Sucursal = "02",
                    Folio = "000123",
                    Disponible = 200m
                };
            }
        }
        public override FormaPago FormaPago => FormaPago.DV;
        private void PagoDevolucionViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(this.Pagar):
                case nameof(this.Devolucion):
                    this.AcceptCommand.RaiseCanExecuteChanged();
                    break;
            }
        }

        protected override void Accept(Utilities.Messages.Pago p)
        {
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(
                    new Utilities.Messages.Pago
                    {
                        FormaPago = FormaPago.DV,
                        Importe = this.Pagar.Value,
                        Sucursal = this.Devolucion.Sucursal,
                        Folio = this.Devolucion.Folio
                    }, this.GID);
        }
        protected override bool CanAccept()
        {
            return this.Devolucion != null
                && (this.Pagar ?? 0) > 0
                && this.Pagar <= this.Devolucion.Disponible;
        }
        #region properties
        private string _sucursal;
        public string Sucursal
        {
            get { return _sucursal; }
            set { this.Set(nameof(this.Sucursal), ref _sucursal, value); }
        }
        private string _folio;
        public string Folio
        {
            get { return _folio; }
            set { this.Set(nameof(this.Folio), ref _folio, value); }
        }
        private Common.Entities.Devolucion _devolucion;
        public Common.Entities.Devolucion Devolucion
        {
            get { return _devolucion; }
            set { this.Set(nameof(this.Devolucion), ref _devolucion, value); }
        }
        #endregion
        #region commands        
        public RelayCommand FindCommand { get; private set; }
        #endregion        
    }
}

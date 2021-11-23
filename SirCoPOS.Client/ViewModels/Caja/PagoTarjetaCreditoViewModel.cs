using SirCoPOS.Common.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using GalaSoft.MvvmLight.Command;
using System.Windows;
using Newtonsoft.Json;

namespace SirCoPOS.Client.ViewModels.Caja
{
    class PagoTarjetaCreditoViewModel : Helpers.PagoViewModel
    {
        public override string Title => "Pago Tarjeta Crédito";
        private Timer tmr;
        private readonly Common.ServiceContracts.IDataServiceAsync _cnn;
        public PagoTarjetaCreditoViewModel()
        {
            this.PropertyChanged += PagoTarjetaCreditoViewModel_PropertyChanged;
            _cnn = CommonServiceLocator.ServiceLocator.Current.GetInstance<Common.ServiceContracts.IDataServiceAsync>();
            this.CobroTerminal = false;
            this.Terminal = true;
            OrderId = null;

            if (this.IsInDesignMode)
            {
                //this.Total = 900;
                this.Pagar = 350;
                this.Terminacion = "1234";
                this.Referencia = "112233";
            } else
            {
                Terminacion = null;
            }
            
            try 
            {
                this.GetCobro = new Helpers.CobroTarjeta();
            }
            catch (ArgumentOutOfRangeException e)
            {
                MessageBox.Show("Problema de comunicación con Terminal", "Pago Tarjeta", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            this.CobroTarjetaCommand = new RelayCommand(() => {
                tmr = new Timer(tmr_Tick, null, 0, 500);
                this.IsBusy= true;
                try
                {
                    if (this.Pagar.HasValue)
                    {
                        GetCobro.Cobrar((double)this.Pagar, "");
                    }
                }
                catch (Exception ec)
                {
                    MessageBox.Show("Problema de comunicación con Terminal", "Pago Tarjeta", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }, () => OrderId == null);

            this.CancelaCobroCommand = new RelayCommand(() =>
            {
                try
                {
                    if (this.OrderId.Length > 0)
                    {
                        GetCobro.Cancela(OrderId);
                    }
                }
                catch (Exception ec)
                {
                    MessageBox.Show("Problema de comunicación con Terminal", "Pago Tarjeta", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }, ()=> OrderId !=null);

            this.ReimprimeCobroCommand= new RelayCommand(() =>
            {
                try
                {
                    if (this.OrderId.Length > 0)
                    {
                        GetCobro.Reimprime(OrderId);
                    }
                }
                catch (Exception ec)
                {
                    MessageBox.Show("Problema de comunicación con Terminal", "Pago Tarjeta", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }, () => OrderId != null);
        }
        private void tmr_Tick(object data)
        {
            string resp = GetCobro.getResponseData();
            if (resp != null)
            {
                tmr.Dispose();
                this.IsBusy = false;

                var request = JsonConvert.DeserializeObject<Common.Entities.OperacionTarjeta>(resp);
                this.OrderId = request.data.orderId;
                this.Terminacion = request.data.cardNumber;
                this.Referencia = request.data.orderId;
                MessageBox.Show(request.data.message, "Net Pay", MessageBoxButton.OK, MessageBoxImage.Information);

                _cnn.RegistraOperacion(request, this.GID);

                Terminal = false;
            }
        }
        public override FormaPago FormaPago => FormaPago.TC;
        protected override void Accept(Utilities.Messages.Pago p)
        {
            try
            { 
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(
                    new Utilities.Messages.Pago
                    {
                        FormaPago = this.FormaPago,
                        Importe = this.Pagar.Value,
                        Terminacion = this.Terminacion,
                        Referencia = this.Referencia,
                        OrderId = this.OrderId
                    }, this.GID);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine("Error: {0}", e);
                throw;
            }
        }
        protected override bool CanAccept()
        {
            try
            { 
                if (!this.IsValid())
                    return false;

                if (this.CobroTerminal == true && this.OrderId == null)
                 {
                    return false;
                 }
                if (this.CobroTerminal == false )
                {
                    if (this.Terminacion == null)
                    {
                        return false;
                    }
                    if (this.Terminacion.Trim().Length != 4)
                    {
                        return false;
                    }
                }
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
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine("Error: {0}", e);
                throw;
            }
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
                case nameof(this.CobroTerminal):
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        this.AcceptCommand.RaiseCanExecuteChanged();
                    });
                    break;
                case nameof(this.OrderId):
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        this.CobroTarjetaCommand.RaiseCanExecuteChanged();
                        this.CancelaCobroCommand.RaiseCanExecuteChanged();
                        this.ReimprimeCobroCommand.RaiseCanExecuteChanged();

                        this.AcceptCommand.RaiseCanExecuteChanged();
                    });
                    break;
                case nameof(this.Terminal):
                    this.RaisePropertyChanged(nameof(this.CobroTerminal));
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
        private bool _cobroterminal;
        public bool CobroTerminal
        {
            get { return _cobroterminal; }
            set { 
                this.Set(nameof(this.CobroTerminal), ref _cobroterminal, value);
                this.Set(nameof(this.CobroSinTerminal), ref _cobrosinterminal, !value);
                if (value)
                {
                    this.Set(nameof(this.Referencia), ref _referencia, "");
                    this.Set(nameof(this.Terminacion), ref _terminacion, null);
                }
            }
        }
        private bool _cobrosinterminal;
        public bool CobroSinTerminal
        {
            get { return !CobroTerminal; }
        }
        private string _orderid;
        public string OrderId
        {
            get { return _orderid; }
            set { 
                this.Set(nameof(this.OrderId), ref _orderid, value); 
            }
        }
        private bool _terminal;
        public bool Terminal
        {
            get { return _terminal; }
            set
            {
                this.Set(nameof(this.Terminal), ref _terminal, value);
            }
        }
        public Helpers.CobroTarjeta _getcobro;
        public Helpers.CobroTarjeta GetCobro
        {
            get { return _getcobro; }
            set { this.Set(nameof(this.GetCobro), ref _getcobro, value); }
        }
        #endregion
        #region computed        
        #endregion
        #region commands
        public RelayCommand CobroTarjetaCommand { get; private set; }
        public RelayCommand CancelaCobroCommand { get; private set; }
        public RelayCommand ReimprimeCobroCommand { get; private set; }
        #endregion
    }
}

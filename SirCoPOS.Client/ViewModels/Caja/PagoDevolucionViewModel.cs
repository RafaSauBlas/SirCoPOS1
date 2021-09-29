using GalaSoft.MvvmLight.Command;
using SirCoPOS.Common.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SirCoPOS.Client.ViewModels.Caja
{
    class PagoDevolucionViewModel : Helpers.PagoViewModel
    {
        public override string Title => "Pago Devolucion";
        private Common.ServiceContracts.IDataServiceAsync _proxy;
        //private Common.ServiceContracts.IProcessServiceAsync _proc;
        public PagoDevolucionViewModel()
        {
            if (!this.IsInDesignMode)
                _proxy = CommonServiceLocator.ServiceLocator.Current.GetInstance<Common.ServiceContracts.IDataServiceAsync>();
            //_proc = CommonServiceLocator.ServiceLocator.Current.GetInstance<Common.ServiceContracts.IProcessServiceAsync>();            
            this.PropertyChanged += PagoDevolucionViewModel_PropertyChanged;

            this.FindCommand = new RelayCommand(async () => {
                
                this.Devolucion = await _proxy.FindDevolucionAsync(this.Sucursal, this.Folio);
                if (this.Devolucion != null)
                {
                    switch (this.Devolucion.Estatus)
                    {
                        case "ZC" :
                            MessageBox.Show("La devolución está cancelada", "Forma de Pago Devolución", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                            break;
                        case "YA":
                            MessageBox.Show("La devolución no tiene monto disponible", "Forma de Pago Devolución", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                            break;
                        default:
                            //if (this.Devolucion.Disponible >= this.Total)
                            //    this.Pagar = this.Total;
                            //else if (this.Devolucion.Disponible > 0)
                            //    this.Pagar = this.Devolucion.Disponible;

                            this.DevSucursal = this.Sucursal;
                            this.DevFolio = this.Devolucion.Folio;
                            this.DevProrrateo = Prorrateo(this.Sucursal, this.Folio);
                            if (this.DevProrrateo  != null)
                            {
                                string tipoPago = "Crédito";
                                if (DevProrrateo == "EF")
                                {
                                    tipoPago = "Contado";
                                }
                                MessageBox.Show("La Devolución se tomará como pago de " + tipoPago + "\n" +
                                                 "para aplicar en Promociones Vigentes", "Pago Devolución", MessageBoxButton.OK, MessageBoxImage.Information);
                                this.ActualizaPromociones();

                                if (this.Devolucion.Disponible >= this.Total)
                                    this.Pagar = this.Total;
                                else if (this.Devolucion.Disponible > 0)
                                    this.Pagar = this.Devolucion.Disponible;

                            }
                            break;



                    }
                }
                else
                {
                    MessageBox.Show("No se encontró la Devolución", "Forma de Pago Devolución", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    this.DevSucursal = null;
                    this.DevFolio = null;
                    this.DevProrrateo = null;
                }
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

        private string Prorrateo(string suc, string folio)
        {
            return _proxy.GetPorcentajeFPago(suc, folio).Select(i => i.FormaPago).SingleOrDefault();
        }

        public override FormaPago FormaPago => FormaPago.DV;
        private void PagoDevolucionViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(this.Pagar):
                case nameof(this.Devolucion):
                case nameof(this.DevProrrateo):
                    this.AcceptCommand.RaiseCanExecuteChanged();
                    break;
            }
        }

        protected override void Accept(Utilities.Messages.Pago p)
        {
            var msg = new Utilities.Messages.Pago {
                FormaPago = FormaPago.DV,
                Importe = this.Pagar.Value,
                Sucursal = this.Devolucion.Sucursal,
                Folio = this.Devolucion.Folio,
            };
            if (this.DevProrrateo == "EF")
            {
                msg.TipoDev = "CO";
            }
            if (this.DevProrrateo == "VA")
            {
                msg.TipoDev = "CR";
            }
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(msg, this.GID);
        }

        protected override bool CanAccept()
        {
            if (this.Devolucion != null)
            {
                if (this.Pagar > this.Devolucion.Disponible)
                    return false;
                if (Devolucion.Estatus == Common.Constants.Status.ZC.ToString())
                    return false;
                if (this.Pagar == 0)
                    return false;
                //var aceptar = this.Devolucion != null
                //&& (this.Pagar ?? 0) > 0
                //&& this.Pagar <= this.Devolucion.Disponible
                //&& this.Devolucion.Estatus != Common.Constants.Status.ZC.ToString();

                return true;
            }
            return false;
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

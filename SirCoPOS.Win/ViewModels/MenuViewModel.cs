using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Win.ViewModels
{
    class MenuViewModel : GalaSoft.MvvmLight.ViewModelBase
    {
        public RelayCommand<Utilities.Constants.TabType> OpenCommand { get; private set; }
        public RelayCommand CloseCommand { get; private set; }
        public RelayCommand LogoutCommand { get; private set; }
        private IDictionary<Utilities.Constants.TabType, bool> _options;
        private readonly Common.ServiceContracts.IAdminServiceAsync _proxy;
        public MenuViewModel()
        {
            _options = new Dictionary<Utilities.Constants.TabType, bool>() {
                { Utilities.Constants.TabType.Caja, true }
                ,{ Utilities.Constants.TabType.Nota, true }
                ,{ Utilities.Constants.TabType.NotaRevision, true }
                ,{ Utilities.Constants.TabType.CreditoPersonal, true }
                ,{ Utilities.Constants.TabType.VerificarVale, true }
                ,{ Utilities.Constants.TabType.VerificarValeExterno, true }
                ,{ Utilities.Constants.TabType.DineroElectronico, true }
                ,{ Utilities.Constants.TabType.Administracion, true }
                ,{ Utilities.Constants.TabType.Cambio, true }
                ,{ Utilities.Constants.TabType.Devolucion, true }
                ,{ Utilities.Constants.TabType.Cancelacion, true }
                ,{ Utilities.Constants.TabType.CancelacionDevolucion, true }
                ,{ Utilities.Constants.TabType.CancelacionCambio, true }

                , { Utilities.Constants.TabType.ConsultaVenta, true }
                , { Utilities.Constants.TabType.ConsultaDevolucion, true }
                , { Utilities.Constants.TabType.ConsultaProducto, true }
                    
                , { Utilities.Constants.TabType.Pagos, true }
                , { Utilities.Constants.TabType.Corte, true }
                , { Utilities.Constants.TabType.Efectivo, true }
                    
                , { Utilities.Constants.TabType.FondoApertura, true }
                , { Utilities.Constants.TabType.FondoCierre, true }
                , { Utilities.Constants.TabType.FondoArqueo, true }
                , { Utilities.Constants.TabType.FondoTransferir, true }
                , { Utilities.Constants.TabType.CambiarResponsable, true }
                , { Utilities.Constants.TabType.Gasto, true }

                , { Utilities.Constants.TabType.Bonos, true }
            };            
            this.OpenCommand = new RelayCommand<Utilities.Constants.TabType>(
                m => {
                    GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(
                        new Messages.MenuItem { Name = m });
                    }, 
                m => _options.ContainsKey(m) && _options[m]
            );

            this.CloseCommand = new RelayCommand(() => {
                System.Windows.Application.Current.Shutdown();
            });
            this.LogoutCommand = new RelayCommand(() => {
                GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(new Utilities.Messages.LogoutTimeout());
                //var ls = new Helpers.LocalStorage();
                //ls.ClearCajero();                
            });

            GalaSoft.MvvmLight.Messaging.Messenger.Default.Register<Utilities.Messages.FondoAperturaCierre>(this, m =>
            {
                foreach (var key in _options.Keys.ToArray())
                {
                    _options[key] = m.Open;
                }
                
                //_options[Utilities.Constants.TabType.Caja] = !m.Open;
                //_options[Utilities.Constants.TabType.Nota] = !m.Open;
                //_options[Utilities.Constants.TabType.NotaRevision] = !m.Open;
                //_options[Utilities.Constants.TabType.CreditoPersonal] = !m.Open;
                //_options[Utilities.Constants.TabType.VerificarVale] = !m.Open;
                //_options[Utilities.Constants.TabType.VerificarValeExterno] = !m.Open;
                //_options[Utilities.Constants.TabType.DineroElectronico] = !m.Open;

                //_options[Utilities.Constants.TabType.FondoApertura] = !m.Open;
                //_options[Utilities.Constants.TabType.Cambio] = !m.Open;
                //_options[Utilities.Constants.TabType.Devolucion] = !m.Open;
                //_options[Utilities.Constants.TabType.Cancelacion] = !m.Open;
                //_options[Utilities.Constants.TabType.CancelacionDevolucion] = !m.Open;
               //_options[Utilities.Constants.TabType.CancelacionCambio] = !m.Open;
                //_options[Utilities.Constants.TabType.ConsultaVenta] = !m.Open;
                //_options[Utilities.Constants.TabType.ConsultaDevolucion] = !m.Open;
                //_options[Utilities.Constants.TabType.ConsultaProducto] = !m.Open;
                //_options[Utilities.Constants.TabType.Pagos] = !m.Open;
                //_options[Utilities.Constants.TabType.Corte] = !m.Open;
                //_options[Utilities.Constants.TabType.Efectivo] = !m.Open;
                _options[Utilities.Constants.TabType.FondoApertura] = !m.Open;
                //_options[Utilities.Constants.TabType.FondoCierre] = !m.Open;
                //_options[Utilities.Constants.TabType.FondoArqueo] = !m.Open;
                //_options[Utilities.Constants.TabType.FondoTransferir] = !m.Open;
                //_options[Utilities.Constants.TabType.CambiarResponsable] = !m.Open;
                //_options[Utilities.Constants.TabType.Gasto] = !m.Open;
                //_options[Utilities.Constants.TabType.Bonos] = !m.Open;
                //_options[Utilities.Constants.TabType.Credit] = !m.Open;
                this.OpenCommand.RaiseCanExecuteChanged();
            });

            if (!this.IsInDesignMode)
            {
                _proxy = CommonServiceLocator.ServiceLocator.Current.GetInstance<Common.ServiceContracts.IAdminServiceAsync>();
                var settings = CommonServiceLocator.ServiceLocator.Current.GetInstance<Utilities.Models.Settings>();
                var isopen = _proxy.IsFondoAbierto(settings.Sucursal.Clave, settings.Cajero.Id);
                GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(new Utilities.Messages.FondoAperturaCierre { Open = isopen });
            }
        }        
    }
}

using GalaSoft.MvvmLight.Command;
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
                { Utilities.Constants.TabType.Caja, false },
                { Utilities.Constants.TabType.Nota, false },
                { Utilities.Constants.TabType.NotaRevision, false },
                { Utilities.Constants.TabType.CreditoPersonal, false },
                { Utilities.Constants.TabType.VerificarVale, false },
                { Utilities.Constants.TabType.VerificarValeExterno, false },
                { Utilities.Constants.TabType.DineroElectronico, false },
                { Utilities.Constants.TabType.Administracion, false },
                { Utilities.Constants.TabType.Cambio, false },
                { Utilities.Constants.TabType.Devolucion, false },
                { Utilities.Constants.TabType.Cancelacion, false },
                { Utilities.Constants.TabType.CancelacionDevolucion, false },
                { Utilities.Constants.TabType.CancelacionCambio, false }

                , { Utilities.Constants.TabType.ConsultaVenta, false }
                , { Utilities.Constants.TabType.ConsultaDevolucion, false }
                , { Utilities.Constants.TabType.ConsultaProducto, true }
                    
                , { Utilities.Constants.TabType.Pagos, false }
                , { Utilities.Constants.TabType.Corte, false }
                , { Utilities.Constants.TabType.Efectivo, false }
                    
                , { Utilities.Constants.TabType.FondoApertura, true }
                , { Utilities.Constants.TabType.FondoCierre, false }
                , { Utilities.Constants.TabType.FondoArqueo, false }
                , { Utilities.Constants.TabType.FondoTransferir, false }
                , { Utilities.Constants.TabType.CambiarResponsable, false }
                , { Utilities.Constants.TabType.Gasto, false }

                , { Utilities.Constants.TabType.Bonos, false }
            };            
            this.OpenCommand = new RelayCommand<Utilities.Constants.TabType>(
                m => {                    
                    GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(
                        new Messages.MenuItem { Name = m });
                    }, 
                m => _options.ContainsKey(m) && _options[m]
            );

            this.CloseCommand = new RelayCommand(() => {
                GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(new Messages.CloseMenu());
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
                _options[Utilities.Constants.TabType.FondoApertura] = !m.Open;                
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

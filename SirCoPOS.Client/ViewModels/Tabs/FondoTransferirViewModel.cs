using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SirCoPOS.Client.ViewModels.Tabs
{
    class FondoTransferirViewModel : Helpers.TabViewModelBase
    {
        private readonly Common.ServiceContracts.IAdminServiceAsync _proxy;
        public FondoTransferirViewModel()
        {
            this.SaveCommand = new RelayCommand(() => {
                var request = new Common.Entities.FondoTransferRequest
                {
                    Importe = this.Importe.Value,
                    UserFrom = this.UserFrom.Value,
                    UserTo = this.UserTo.Value                    
                };
                _proxy.TransferirFondo(request);
                MessageBox.Show("ready");
            });

            if (this.IsInDesignMode)
            {
                UserFrom = 2;
                UserTo = 3;
                Importe = 100;
            }
            else
            {
                _proxy = CommonServiceLocator.ServiceLocator.Current.GetInstance<Common.ServiceContracts.IAdminServiceAsync>();
            }
        }
        public int? UserFrom { get; set; }
        public int? UserTo { get; set; }        
        public decimal? Importe { get; set; }        
        public RelayCommand SaveCommand { get; private set; }
    }
}

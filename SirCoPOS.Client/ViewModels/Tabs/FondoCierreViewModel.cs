using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SirCoPOS.Client.ViewModels.Tabs
{
    class FondoCierreViewModel : Helpers.TabViewModelBase
    {
        private readonly Common.ServiceContracts.IAdminServiceAsync _proxy;
        public FondoCierreViewModel()
        {
            this.SaveCommand = new RelayCommand(() => {
                var request = new Common.Entities.FondoArqueoRequest
                {
                    Importe = this.Importe.Value,
                    Auditor = this.Auditor.Value,
                    Responsable = this.Responsable.Value
                };
                _proxy.CierreFondo(request);
                MessageBox.Show("ready");
                GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(new Utilities.Messages.FondoAperturaCierre { Open = false });
            });
            if (this.IsInDesignMode)
            {
                this.Responsable = 1;
                this.Auditor = 2;
                this.Importe = 100;
            }
            else
            {
                _proxy = CommonServiceLocator.ServiceLocator.Current.GetInstance<Common.ServiceContracts.IAdminServiceAsync>();
                this.Responsable = this.Cajero.Id;
            }
        }
        public int? Responsable { get; set; }
        public int? Auditor { get; set; }
        public decimal? Importe { get; set; }
        public RelayCommand SaveCommand { get; private set; }
    }
}

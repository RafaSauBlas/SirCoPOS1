using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SirCoPOS.Client.ViewModels.Tabs
{
    class CambiarResponsableViewModel : EntregaEfectivoViewModel
    {        
        public CambiarResponsableViewModel()
        {
            this.LoadAuditorCommand = new RelayCommand(() => {
                this.Auditor = _pdata.FindAuditorTransferir(this.SearchAuditor.Value, this.Cajero.Id);
                if (this.Auditor != null)
                {
                    this.SearchAuditor = null;
                }
            }, () => this.SearchAuditor.HasValue);

            this.SaveCommand = new RelayCommand(() => {

                //var code = Microsoft.VisualBasic.Interaction.InputBox("Codigo Auditor:");
                //var isValid = _proxy.ValidarCodigo(this.Auditor.Id, code);
                //if (!isValid)
                //{
                //   MessageBox.Show("Código no valido.","Error",MessageBoxButton.OK,MessageBoxImage.Error);
                //    return;
                //}

                var request = new Common.Entities.EntregaRequest
                {
                    Sucursal = this.Sucursal.Clave,
                    CajeroId = this.Cajero.Id,
                    AuditorId = this.Auditor.Id,
                    Entregar = this.Entrega.Value,
                    FormasPago = this.FormasPago.Select(i =>
                        new Common.Entities.EntregaFormaPago
                        {
                            FormaPago = i.CajaFormaPago.FormaPago,
                            Entregar = i.Entregar.Value,
                            Amount = i.EntregarMonto.Value
                        })
                };
                _proxy.CorteTransferir(request);
                string msg = "El cambio se ha realizado correctamente";
                string info = "Cambio Responsable";
                MessageBox.Show(msg, info, MessageBoxButton.OK, MessageBoxImage.Information);
                this.CloseCommand.Execute(null);
                GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(new Utilities.Messages.FondoAperturaCierre { Open = false });
            }, () => this.Auditor != null && this.Entrega.HasValue
                && this.FormasPago.All(i => i.Entregar.HasValue && i.EntregarMonto.HasValue));
        }
    }
}

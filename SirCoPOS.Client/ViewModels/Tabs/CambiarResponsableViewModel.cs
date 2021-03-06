using GalaSoft.MvvmLight.Command;
using System;
using GalaSoft.MvvmLight.Messaging;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SirCoPOS.Client.ViewModels.Tabs
{
    class CambiarResponsableViewModel : EntregaEfectivoViewModel
    {
        private Utilities.Models.Settings settings;
        public CambiarResponsableViewModel()
        {
            settings = CommonServiceLocator.ServiceLocator.Current.GetInstance<Utilities.Models.Settings>();
            this.LoadAuditorCommand = new RelayCommand(() => {
                if(this.SearchAuditor.Value == this.Cajero.Id)
                {
                    MessageBox.Show("No puedes utilizar tu propio ID como responsable, por favor introduce el ID de algun responsable valido", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    Messenger.Default.Send<string>("focus", "FocusResponsable");
                }
                else
                {
                    try
                    {
                        this.Auditor = _pdata.FindAuditorTransferir(settings.Sucursal.Clave, this.SearchAuditor.Value, this.Cajero.Id);
                    }
                    catch (System.Exception e)
                    {
                        MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                        return;
                    }
                    if (this.Auditor != null)
                    {
                        if (this.Auditor.Sucursal == settings.Sucursal.Clave)
                        {
                            this.SearchAuditor = null;
                        }
                        else
                        {
                            MessageBox.Show("Auditor no pertenece a la misma sucursal", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            this.Auditor = null;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Auditor no valido.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
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

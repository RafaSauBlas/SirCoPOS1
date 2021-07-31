using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SirCoPOS.Common.Entities;
using System.Windows;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace SirCoPOS.Client.Helpers
{
    class ServiceClient : System.ServiceModel.ClientBase<SirCoPOS.Common.ServiceContracts.IProcessServiceAsync>
    {
        private Utilities.Models.Settings _settings;
        public ServiceClient()
        {
            _settings = CommonServiceLocator.ServiceLocator.Current.GetInstance<Utilities.Models.Settings>();
        }
        private T Exec<T>(Func<T> action)
        {
            using (new OperationContextScope(this.InnerChannel))
            {
                HttpRequestMessageProperty requestMessage;
                if (OperationContext.Current.OutgoingMessageProperties.ContainsKey(HttpRequestMessageProperty.Name))
                    requestMessage = (HttpRequestMessageProperty)OperationContext.Current.OutgoingMessageProperties[HttpRequestMessageProperty.Name];
                else
                {
                    requestMessage = new HttpRequestMessageProperty();
                    OperationContext.Current.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = requestMessage;
                }
                requestMessage.Headers["UserId"] = _settings.Cajero.Id.ToString();
                requestMessage.Headers["Sucursal"] = _settings.Sucursal.Clave;
                return action();
            }
        }
        private async Task Reply(Func<Task<Response>> func)
        {
            var res = await this.Exec(() => func());
            if (res.Success)
                return;
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(
                new Utilities.Messages.MessageBox
                {
                    Text = res.Error
                });
            return;
        }
        private async Task<T> Reply<T>(Func<Task<Response<T>>> func)
        {
            var res = await this.Exec(() => func());
            if (res.Success)
                return res.Item;
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(
                new Utilities.Messages.MessageBox {
                    Text = res.Error
                });
            return default(T);
        }
        public async Task<SaleResponse> SaleAsync(SaleRequest item)
        {
            return await this.Reply(() => this.Channel.SaleAsync(item));
        }
        public async Task CancelSaleAsync(CancelSaleRequest item)
        {
            await this.Reply(() => this.Channel.CancelSaleAsync(item));
        }
        public async Task CancelReturnAsync(string sucursal, string folio)
        {
            await this.Reply(() => this.Channel.CancelReturnAsync(sucursal, folio));
        }
        public async Task<string> ReturnAsync(ReturnRequest item)
        {
            return await this.Reply(() => this.Channel.ReturnAsync(item));
        }
        public async Task<ChangeResponse> ChangeAsync(ChangeRequest model)
        {
            return await this.Reply(() => this.Channel.ChangeAsync(model));
        }
        public async Task<bool> RequestProductoAsync(string serie)
        {
            return await this.Reply(() => this.Channel.RequestProductoAsync(serie));
        }
        public async Task<IEnumerable<Common.Entities.Agrupacion>> GetAgrupacionesPorSerieAsync(string serie)
        {
            return await this.Reply(() => this.Channel.GetAgrupacionesPorSerieAsync(serie));
        }
        public async Task<IEnumerable<Common.Entities.PorcentajeFormaPago>> GetPorcentajePorFPagoAsync(string sucursal, string devolucion)
        {
            return await this.Reply(() => this.Channel.GetPorcentajePorFPagoAsync(sucursal, devolucion));
        }
        public async Task ReleaseProductoAsync(string serie)
        {
            await this.Reply(() => this.Channel.ReleaseProductoAsync(serie));
        }
        public async Task<string> RegisterNoteAsync(int id)
        {
            return await this.Reply(() => this.Channel.RegisterNoteAsync(id));
        }
        public async Task<RegisterValeResponse> RegisterValeAsync(RegisterValeRequest item)
        {
            return await this.Reply(() => this.Channel.RegisterValeAsync(item));
        }
    }
}

using SirCoPOS.Common.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Services
{
    public class ProcessService : BaseService, Common.ServiceContracts.IProcessService
    {
        private static readonly object _sync;
        private readonly BusinessLogic.Process _process;
        static ProcessService()
        {
            _sync = new object();
        }
        public ProcessService()
        {
            _process = new BusinessLogic.Process();
        }        
        public Response ReleaseProducto(string serie)
        {
            var uid = this.GetUserIdOrDefault() ?? 0;
            return this.Reply(() => _process.ReleaseProducto(serie, uid));
        }

        public Response<bool> RequestProducto(string serie)
        {
            var uid = this.GetUserIdOrDefault() ?? 0;
            return this.Reply(() => _process.RequestProducto(serie, uid));
        }
        public Response<string> Return(ReturnRequest item)
        {
            var uid = this.GetUserId();
            var suc = this.GetSucursal();
            return this.Reply(() => _process.Return(item, uid, suc));
        }
        public Response<SaleResponse> Sale(SaleRequest item)
        {
            var uid = this.GetUserIdOrDefault() ?? 0;
            var res = this.Reply(() => {
                var multiple = false;
                if (item.Tipo == SaleType.Sale)
                {
                    var response = _process.Sale(item, uid);
                    if (response.Multiple)
                    {
                        item.Tipo = SaleType.Note;
                        multiple = true;
                    }
                    else
                        return response;
                }
                if (item.Tipo == SaleType.Note)
                {
                    var xml = BusinessLogic.Helpers.Serializer.Serialize(item);
                    var ctx = new DataAccess.SirCoPOSDataContext();
                    var eitem = new DataAccess.SirCoPOS.Nota
                    {
                        Date = DateTime.Now, 
                        CajeroId = uid, 
                        Sucursal = item.Sucursal,
                        Data = xml,
                        Multiple = multiple
                    };
                    ctx.Notas.Add(eitem);
                    ctx.SaveChanges();
                    return new SaleResponse
                    {
                        Folio = $"Nota-{eitem.Id}"
                    };
                }
                return null;
            });
            if (res.Success)
            {
                //var txt = System.Web.HttpUtility.UrlEncode($"Gracias por tu compra, folio: {res.Item.Folio}");
                //var url = $"http://dev.itnnovation.net:81/FCM/api/sendMessage?number={8112123587}&txt={txt}";
                var url = $"http://dev.itnnovation.net:81/FCM/api/sendMessage";

                var client = new WebClient();
                client.QueryString.Add("number", "8112123587");
                client.QueryString.Add("txt", $"Gracias por tu compra, folio: {res.Item.Folio}");
                var reqparm = new System.Collections.Specialized.NameValueCollection();
                //var response = client.UploadValues(url, "POST", reqparm);
            }
            return res;
        }
        public Response CancelReturn(string sucursal, string folio)
        {
            var uid = this.GetUserId();
            return this.Reply(() => _process.CancelReturn(sucursal, folio, uid));
        }
        public Response CancelSale(CancelSaleRequest item)
        {
            var uid = this.GetUserId();
            return this.Reply(() => _process.CancelSale(item, uid));
        }        
        public Response<ChangeResponse> Change(Common.Entities.ChangeRequest model)
        {
            var uid = this.GetUserId();
            var suc = this.GetSucursal();
            return this.Reply(() => _process.Change(model, uid, suc));
        }
        public Response<string> RegisterNote(int id)
        {
            var uid = this.GetUserId();
            return this.Reply(() => _process.RegisterNote(id, uid));
        }
        public Response<RegisterValeResponse> RegisterVale(RegisterValeRequest item)
        {
            var uid = this.GetUserId();
            var suc = this.GetSucursal();
            return this.Reply(() => _process.RegistrarVale(item, uid, suc));
        }
        private Response Reply(Action action)
        {
            lock (_sync)
            {
                using (var tran = new System.Transactions.TransactionScope())
                {
                    var res = new Response();
                    try
                    {
                        action();
                        tran.Complete();
                        res.Success = true;
                    }
                    catch (BusinessLogic.CustomException ex)
                    {
                        res.Success = false;
                        res.Error = ex.Message;
                    }
                    return res;
                }
            }
        }
        private Response<T> Reply<T>(Func<T> func)
        {
            lock (_sync)
            {
                using (var tran = new System.Transactions.TransactionScope())
                {
                    var res = new Response<T>();
                    try
                    {
                        res.Item = func();
                        tran.Complete();
                        res.Success = true;
                    }
                    catch (BusinessLogic.CustomException ex)
                    {
                        res.Success = false;
                        res.Error = ex.Message;
                    }
                    return res;
                }
            }
        }
    }
}

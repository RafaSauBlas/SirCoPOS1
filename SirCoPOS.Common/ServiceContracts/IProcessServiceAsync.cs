using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Common.ServiceContracts
{
    [ServiceContract(Name = "IProcessService")]
    public interface IProcessServiceAsync : IProcessService
    {
        [OperationContract]
        Task<Common.Entities.Response<Entities.SaleResponse>> SaleAsync(Entities.SaleRequest item);
        [OperationContract]
        Task<Entities.Response<string>> ReturnAsync(Entities.ReturnRequest item);
        [OperationContract]
        Task<Entities.Response> CancelSaleAsync(Entities.CancelSaleRequest item);
        [OperationContract]
        Task<Entities.Response> CancelReturnAsync(string sucursal, string folio);
        [OperationContract]
        Task<Entities.Response<Entities.ChangeResponse>> ChangeAsync(Entities.ChangeRequest model);
        [OperationContract]
        Task<Entities.Response<bool>> RequestProductoAsync(string serie);
        [OperationContract]
        Task<Entities.Response> ReleaseProductoAsync(string serie);
        [OperationContract]
        Task<Entities.Response<string>> RegisterNoteAsync(int id);
        [OperationContract]
        Task<Entities.Response<Entities.RegisterValeResponse>> RegisterValeAsync(Entities.RegisterValeRequest item);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Common.ServiceContracts
{
    [ServiceContract]
    public interface IProcessService
    {
        [OperationContract]
        Entities.Response<Entities.SaleResponse> Sale(Entities.SaleRequest item);
        [OperationContract]
        Entities.Response<string> Return(Entities.ReturnRequest item);
        [OperationContract]
        Entities.Response CancelSale(Entities.CancelSaleRequest item);
        [OperationContract]
        Entities.Response CancelReturn(string sucursal, string folio);
        [OperationContract]
        Entities.Response<Entities.ChangeResponse> Change(Entities.ChangeRequest model);
        [OperationContract]
        Entities.Response<bool> RequestProducto(string serie);
        [OperationContract]
        Entities.Response<IEnumerable<Entities.Agrupacion>> GetAgrupacionesPorSerie(string serie);
        [OperationContract]
        Entities.Response ReleaseProducto(string serie);
        [OperationContract]
        Entities.Response<string> RegisterNote(int id);
        [OperationContract]
        Entities.Response<Entities.RegisterValeResponse> RegisterVale(Entities.RegisterValeRequest item);
    }
}

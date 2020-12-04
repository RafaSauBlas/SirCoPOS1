using SirCoPOS.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Common.ServiceContracts
{
    [ServiceContract(Name = "ICreditoValeService")]
    public interface ICreditoValeServiceAsync : ICreditoValeService
    {
        [OperationContract]
        Task<SolicitudCreditoResponse> RequestAsync(SolicitudCreditoRequest item);
        [OperationContract]
        Task UpdateAsync(Guid gid, int uid);
        [OperationContract]
        Task CompleteAsync(ReplyCreditoRequest item);
    }
}

using SirCoPOS.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Common.ServiceContracts
{
    [ServiceContract]
    public interface ICreditoValeService
    {
        [OperationContract]
        SolicitudCreditoResponse Request(SolicitudCreditoRequest item);
        [OperationContract]
        void Update(Guid gid, int uid);
        [OperationContract]
        void Complete(ReplyCreditoRequest item);
    }
}

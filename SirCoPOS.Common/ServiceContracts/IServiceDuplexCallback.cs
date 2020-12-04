using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Common.ServiceContracts
{
    public interface IServiceDuplexCallback
    {
        [OperationContract(IsOneWay = true)]
        void Receive(string msg);
        [OperationContract(IsOneWay = true)]
        void Response(bool result);
        [OperationContract(IsOneWay = true)]
        void Update(Guid gid, bool complete);
    }
}

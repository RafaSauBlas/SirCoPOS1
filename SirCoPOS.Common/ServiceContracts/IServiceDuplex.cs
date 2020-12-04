using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Common.ServiceContracts
{
    [ServiceContract(SessionMode = SessionMode.Required, CallbackContract = typeof(IServiceDuplexCallback))]
    public interface IServiceDuplex
    {
        [OperationContract(IsOneWay = true, IsInitiating = true)]
        void Connect(string id);        
        [OperationContract]
        void Ping();
    }
    [ServiceContract]
    public interface IServiceDuplexReply
    {
        [OperationContract(IsOneWay = true)]
        void Send(string id, string msg);
        [OperationContract(IsOneWay = true)]
        void Update(string id, Guid gid, bool complete);
    }
}

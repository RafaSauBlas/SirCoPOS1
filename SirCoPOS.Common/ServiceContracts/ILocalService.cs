using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Common.ServiceContracts
{
    [ServiceContract]
    public interface ILocalService
    {
        [OperationContract]
        bool Match(byte[] current, byte[] compare);
        [OperationContract]
        int? Find(byte[] current, IDictionary<int, byte[]> options);
        [OperationContract]
        bool Send(string number, string message);
    }
}

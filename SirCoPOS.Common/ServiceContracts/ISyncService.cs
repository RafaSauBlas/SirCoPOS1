using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Common.ServiceContracts
{
    [ServiceContract]
    public interface ISyncService
    {
        [OperationContract]
        IEnumerable<Entities.FileSync> GetFiles();
        [OperationContract]
        Stream GetFile(string filename);
    }
}

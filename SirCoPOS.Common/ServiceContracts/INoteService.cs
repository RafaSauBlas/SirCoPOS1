using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Common.ServiceContracts
{
    [ServiceContract]
    public interface INoteService
    {
        [OperationContract]
        int SaveNote(Entities.NoteRequest request);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Common.ServiceContracts
{
    [ServiceContract]
    public interface IPrintService
    {
        [OperationContract]
        Common.Entities.Reports.ReciboCompraReport GetReciboCompra(string sucursal, string folio);
        [OperationContract]
        Common.Entities.Reports.ReciboDevolucionReport GetReciboDevolucion(string sucursal, string folio);
        [OperationContract]
        Common.Entities.Reports.ReciboContraValeReport GetContraVale(string sucursal, string folio);
    }
}

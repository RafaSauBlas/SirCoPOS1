using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Text;

namespace SirCoPOS.Contracts.Services
{
    [ServiceContract]
    public interface IService
    {
        [OperationContract]
        Entities.ScanResponse ScanProducto(string serie, string sucursal);
        [OperationContract]
        Entities.SaleResponse Sale(Entities.SaleRequest request);
    }
}

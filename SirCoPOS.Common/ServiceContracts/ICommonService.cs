using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Common.ServiceContracts
{
    [ServiceContract]
    public interface ICommonService
    {
        [OperationContract]
        Entities.Empleado Login(string sucursal, string user, string pass);
        [OperationContract]
        Entities.Empleado FindVendedor(int id);
        [OperationContract]
        Entities.Empleado FindCajero(string user);
        [OperationContract]
        Common.Entities.Sucursal FindSucursal(string sucursal);
        [OperationContract]
        Entities.Empleado CheckFingerLogin(string sucursal, byte[] huella);
        [OperationContract]
        Entities.Empleado CheckFingerAdmin(byte[] huella);
        [OperationContract]
        byte[] GetFingerEmpleado(int idempleado);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Common.ServiceContracts
{
    [ServiceContract(Name = "ICommonService")]
    public interface ICommonServiceAsync : ICommonService
    {
        [OperationContract]
        Task<Entities.Empleado> LoginAsync(string sucursal, string user, string pass);
        [OperationContract]
        Task<int> TimeOutAsync();
        [OperationContract]
        Task<Entities.Empleado> FindVendedorAsync(int id);
        [OperationContract]
        Task<Entities.Empleado> FindCajeroAsync(string user);
        [OperationContract]
        Task<Entities.Empleado> FindEmpleadoAsync(string user);
        [OperationContract]
        Task<Entities.Empleado> FindEmpleadoAbonoAsync(int idEmpleado);
    }
}

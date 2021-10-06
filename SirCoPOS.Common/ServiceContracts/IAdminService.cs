using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Common.ServiceContracts
{
    [ServiceContract]
    public interface IAdminService
    {
        [OperationContract]
        decimal? GetDisponibleFondo(int id);
        [OperationContract]
        IEnumerable<Common.Entities.Option> GetTiposGasto();
        [OperationContract]
        void GenerarGasto(Entities.GastoRequest request);
        [OperationContract]
        bool ValidarCodigo(int id, string token);
        [OperationContract]
        void AbrirFondo(Entities.FondoRequest request);
        [OperationContract]
        void ArqueoFondo(Entities.FondoArqueoRequest request);
        [OperationContract]
        void CierreFondo(Entities.FondoArqueoRequest request);
        [OperationContract]
        void TransferirFondo(Entities.FondoTransferRequest request);
        [OperationContract]
        Common.Entities.Bonos GetBonos(int empleado);
        [OperationContract]
        bool PayBono(int gerente, int empleado, decimal importe);
        [OperationContract]
        void PagoBonos(int empleado, int supervisor, decimal importe);
        [OperationContract]
        void Corte(Entities.CorteRequest request);
        [OperationContract]
        void CorteTransferir(Entities.EntregaRequest request);
        [OperationContract]
        void Entrega(Entities.EntregaRequest request);
        [OperationContract]
        Entities.CorteResponse GetCorteCaja(string sucursal, int idcajero);
        [OperationContract]
        Entities.CajaFormas GetEntrega(string sucursal, int idgerente);
        //[OperationContract]
        //decimal? GetEfectivoCaja(string sucursal, int idcajero);
        [OperationContract]
        bool IsFondoAbierto(string sucursal, int idcajero);
        [OperationContract]
        IEnumerable<Common.Entities.Caja> GetCajas(string sucursal, int idempleado);
        [OperationContract]
        byte[] GetHuella(int idempleado);
        [OperationContract]
        int? IdentificarSupervisor(string sucursal, byte[] template);
    }
}

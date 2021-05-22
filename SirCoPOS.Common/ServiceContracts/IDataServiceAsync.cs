using SirCoPOS.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Common.ServiceContracts
{
    [ServiceContract(Name = "IDataService")]
    public interface IDataServiceAsync : IDataService
    {        
        [OperationContract]
        Task<Entities.ScanResponse> ScanProductoAsync(string serie, string sucursal);
        [OperationContract]
        Task<Entities.ScanDevolucionResponse> ScanProductoDevolucionAsync(string serie, bool cancelacion);        
        [OperationContract]
        Task<Entities.DevolucionResponse> FindSaleAsync(string sucursal, string folio);
        [OperationContract]
        Task<Common.Entities.Devolucion> FindDevolucionAsync(string sucursal, string folio);
        [OperationContract]
        Task<Common.Entities.ValeResponse> FindValeAsync(string vale);
        [OperationContract]
        //Task<Common.Entities.Cliente> FindClienteAsync(string nombre);
        //[OperationContract]
        Task<Common.Entities.ValeResponse> FindValeDigitalAsync(string vale);
        [OperationContract]
        Task<Common.Entities.ValeResponse> FindValeDigitalByClientAsync(int id);
        [OperationContract]
        Task<Common.Entities.CValeResponse> FindContraValeAsync(string sucursal, string vale);
        [OperationContract]
        Task<ValeResponse> FindTarjetahabienteAsync(string id);
        [OperationContract]
        Task<Common.Entities.PromocionesValeResponse> FindPromocionesValeAsync(string sucursal);
        [OperationContract]
        Task<Common.Entities.PromocionesCreditoResponse> FindPromocionesCreditoAsync(string sucursal);
        [OperationContract]
        Task<CheckPromocionesCuponesResponse> CheckPromocionesAsync(CheckPromocionesCuponesRequest request);
        [OperationContract]
        Task<IEnumerable<Common.Entities.Colonia>> FindColoniasAsync(string cp);
        [OperationContract]
        Task<Common.Entities.Cupon> FindCuponAsync(string folio);
        [OperationContract]
        Task<IEnumerable<Common.Entities.PromocionCupon>> FindCuponesByClienteAsync(int clienteId);
        [OperationContract]
        Task<IEnumerable<Common.Entities.Promocion>> GetPromocionesAsync(CheckPromocionesRequest request);
        [OperationContract]
        Task<IEnumerable<Common.Constants.FormaPago>> GetFormasPagoAsync();
        [OperationContract]
        Task<IDictionary<int, ICollection<byte[]>>> ApproveAsync(string sucursal);
        [OperationContract]
        Task<Common.Entities.ValeResponse> FindDistribuidorAsync(string id);
        [OperationContract]
        Task<Common.Entities.ValeResponse> FindDistribuidorIdAsync(int? id);
        [OperationContract]
        Task<decimal?> FindMonederoAsync(int cliente);
        [OperationContract]
        Task<Common.Entities.MedidasCorridas> GetPreciosAsync(int id);
        [OperationContract]
        Task<IEnumerable<Common.Entities.SucursalExistencia>> GetExistenciasAsync(int id, string medida);
    }
}

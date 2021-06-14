using SirCoPOS.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Common.ServiceContracts
{
    [ServiceContract]
    public interface IDataService
    {
        [OperationContract]
        void test();
        [OperationContract]
        Empleado FindAuditorApertura(int id, int idcajero);
        [OperationContract]
        Empleado FindAuditorEntrega(int id, int idcajero);
        [OperationContract]
        Empleado FindAuditorTransferir(int id, int idcajero);
        [OperationContract]
        Entities.ScanResponse ScanProducto(string serie, string sucursal);
        [OperationContract]
        Producto FindProducto(string marca, string modelo, string sucursal);
        [OperationContract]
        Entities.ScanDevolucionResponse ScanProductoDevolucion(string serie, bool cancelacion);
        [OperationContract]
        Entities.ScanDevolucionResponse ScanProductoFromDevolucion(string serie);
        [OperationContract]
        Entities.DevolucionResponse FindSale(string sucursal, string folio);        
        [OperationContract]
        Common.Entities.ValeResponse FindDistribuidor(string id);
        [OperationContract]
        Common.Entities.ValeResponse FindDistribuidorId(int? id);
        [OperationContract]
        IEnumerable<Common.Entities.Colonia> FindColonias(string cp);
        //[OperationContract]
        //int AddCliente(Common.Entities.Cliente model);
        [OperationContract]
        CheckPromocionesCuponesResponse CheckPromociones(CheckPromocionesCuponesRequest request);        
        [OperationContract]
        Common.Entities.ValeResponse FindVale(string vale);
        [OperationContract]
        Common.Entities.ValeResponse FindValeDigital(string vale);
        [OperationContract]
        Common.Entities.ValeResponse FindValeDigitalByClient(int id);
        [OperationContract]
        Common.Entities.CValeResponse FindContraVale(string sucursal, string vale);
        [OperationContract]
        ValeResponse FindTarjetahabiente(string id);
        [OperationContract]
        Common.Entities.PromocionesValeResponse FindPromocionesVale(string sucursal);
        [OperationContract]
        Common.Entities.PromocionesCreditoResponse FindPromocionesCredito(string sucursal);
        [OperationContract]
        Common.Entities.Devolucion FindDevolucion(string sucursal, string folio);
        [OperationContract]
        Common.Entities.Cupon FindCupon(string folio);
        [OperationContract]
        Common.Entities.Cliente FindCliente(int? id, string telefono = null, string nombre = null);
        [OperationContract]
        List<Cliente> FindCliente2(string telefono = null, string nombre = null, string appa = null, string apma = null);
        [OperationContract]
        string FindColonia(int id);
        [OperationContract]
        string FindCiudad(int id);
        [OperationContract]
        string FindEstado(int id);
        [OperationContract]
        Common.Entities.Cliente FindClienteByCode(Guid code);
        [OperationContract]
        IEnumerable<Common.Entities.PromocionCupon> FindCuponesByCliente(int clienteId);
        [OperationContract]
        IEnumerable<Common.Entities.Promocion> GetPromociones(CheckPromocionesRequest request);
        [OperationContract]
        IEnumerable<Common.Constants.FormaPago> GetFormasPago();
        [OperationContract]
        IDictionary<int, ICollection<byte[]>> Approve(string sucursal);
        [OperationContract]
        decimal? FindMonedero(int cliente);
        [OperationContract]
        IEnumerable<Common.Entities.NegocioExterno> GetNegocios();
        [OperationContract]
        Common.Entities.ValeResponse FindDistribuidorExterno(int idnegocio, string nocuenta, string vale);
        [OperationContract]
        IEnumerable<Common.Entities.DescuentoAdicional> GetDescuentoAdicionals();
        [OperationContract]
        IEnumerable<Common.Entities.RazonNotaDevolucion> GetRazonesDevolucion();
        [OperationContract]
        IEnumerable<Common.Entities.RazonNotaDevolucion> GetRazonesNotas();
        [OperationContract]
        Common.Entities.VentaView FindVentaView(string sucursal, string folio, int idcajero);
        [OperationContract]
        Common.Entities.DevolucionView FindDevolucionView(string sucursal, string folio, int idcajero);
        [OperationContract]
        IEnumerable<Common.Entities.NoteHeader> GetNotes();
        [OperationContract]
        IEnumerable<Common.Entities.NoteDetalle> GetNoteDetails(int id);
        [OperationContract]
        bool CheckCelular(string celular);
        [OperationContract]
        Common.Entities.MedidasCorridas GetPrecios(int id);
        [OperationContract]
        IEnumerable<Common.Entities.SucursalExistencia> GetExistencias(int id, string medida);
        [OperationContract]
        IDictionary<DateTime, decimal> GenerarPlanPagosFechas(int iddist, Pago item);
    }
}

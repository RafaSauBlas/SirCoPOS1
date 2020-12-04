using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Client.Helpers
{
    class ReportsHelper
    {
        private Common.ServiceContracts.IPrintServiceAsync _proxy;
        private AutoMapper.IMapper _mapper;
        private Utilities.Interfaces.IReportViewer _viewer;
        public ReportsHelper()
        {
            _proxy = CommonServiceLocator.ServiceLocator.Current.GetInstance<Common.ServiceContracts.IPrintServiceAsync>();
            _mapper = CommonServiceLocator.ServiceLocator.Current.GetInstance<AutoMapper.IMapper>();
            _viewer = CommonServiceLocator.ServiceLocator.Current.GetInstance<Utilities.Interfaces.IReportViewer>();
        }
        public void Compra(string sucursal, string folio)
        {
            var venta = _proxy.GetReciboCompra(sucursal, folio);

            var item = _mapper.Map<SirCoPOS.Reports.Entities.ReciboCompra>(venta.Recibo);
            var productos = _mapper.Map<IEnumerable<SirCoPOS.Reports.Entities.Producto>>(venta.Productos);
            var pagos = _mapper.Map<IEnumerable<SirCoPOS.Reports.Entities.Pago>>(venta.Pagos);
            var planPagos = _mapper.Map<IEnumerable<SirCoPOS.Reports.Entities.PlanPago>>(venta.PlanPagos);

            //item.CantidadLetra = item.Cantidad.ToLetras();
            var list = new List<SirCoPOS.Reports.Entities.ReciboCompra>() { item };
            var dic = new Dictionary<string, IEnumerable<object>>() {
                { "reciboDataSet", list },
                { "productosDataSet", productos },
                { "pagosDataSet", pagos },
                { "planPagosDataSet", planPagos }
            };
            _viewer.OpenViewer(
                fullname: "SirCoPOS.Reports.ReciboVenta.rdlc",
                library: "SirCoPOS.Reports",
                datasources: dic);            
        }
        public void Devolucion(string sucursal, string folio)
        {
            var devolucion = _proxy.GetReciboDevolucion(sucursal, folio);

            var item = _mapper.Map<SirCoPOS.Reports.Entities.ReciboDevolucion>(devolucion.Recibo);
            var productos = _mapper.Map<IEnumerable<SirCoPOS.Reports.Entities.Producto>>(devolucion.Productos);

            var list = new List<SirCoPOS.Reports.Entities.ReciboDevolucion>() { item };
            var dic = new Dictionary<string, IEnumerable<object>>() {
                { "devolucionDataSet", list },
                { "productosDataSet", productos }
            };
            _viewer.OpenViewer(
                fullname: "SirCoPOS.Reports.ReciboDevolucion.rdlc",
                library: "SirCoPOS.Reports",
                datasources: dic);
        }
    }
}

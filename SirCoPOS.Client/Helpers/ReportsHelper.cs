using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Reporting.WinForms;


namespace SirCoPOS.Client.Helpers
{
    class ReportsHelper
    {
        private Common.ServiceContracts.IPrintServiceAsync _proxy;
        private AutoMapper.IMapper _mapper;
        private Utilities.Interfaces.IReportViewer _viewer;
        private readonly Common.ServiceContracts.IDataServiceAsync _data;
        public ReportsHelper()
        {
            _proxy = CommonServiceLocator.ServiceLocator.Current.GetInstance<Common.ServiceContracts.IPrintServiceAsync>();
            _mapper = CommonServiceLocator.ServiceLocator.Current.GetInstance<AutoMapper.IMapper>();
            _viewer = CommonServiceLocator.ServiceLocator.Current.GetInstance<Utilities.Interfaces.IReportViewer>();
            _data = CommonServiceLocator.ServiceLocator.Current.GetInstance<Common.ServiceContracts.IDataServiceAsync>();
        }


        public void Cancelacion (string sucursal, string folio)
        {
            var cancela = _proxy.GetReciboCancelacion(sucursal, folio);
            var item = _mapper.Map<SirCoPOS.Reports.Entities.ReciboCancelacion>(cancela.Recibo);
            var productos = _mapper.Map<IEnumerable<SirCoPOS.Reports.Entities.Producto>>(cancela.Productos);

            var list = new List<SirCoPOS.Reports.Entities.ReciboCancelacion>() { item };
            var dic = new Dictionary<string, IEnumerable<object>>() {
                { "reciboDataSet", list },
                { "productosDataSet", productos },
            };
            if (System.Diagnostics.Debugger.IsAttached)
            {
                _viewer.OpenViewer(
                fullname: "SirCoPOS.Reports.ReciboCancelacion.rdlc",
                library: "SirCoPOS.Reports",
                datasources: dic);
            }
            else { 
            var pd = new Helpers.PrintFile(
                fullname: "SirCoPOS.Reports.ReciboCancelacion.rdlc",
                library: "SirCoPOS.Reports",
                datasources: dic);
            pd.Print();
            }
        }


        public void Compra(string sucursal, string folio, bool reimpresion = false)
        {
            var venta = _proxy.GetReciboCompra(sucursal, folio, reimpresion);

            var item = _mapper.Map<SirCoPOS.Reports.Entities.ReciboCompra>(venta.Recibo);
            var productos = _mapper.Map<IEnumerable<SirCoPOS.Reports.Entities.Producto>>(venta.Productos);
            var pagos = _mapper.Map<IEnumerable<SirCoPOS.Reports.Entities.Pago>>(venta.Pagos);
            var planPagos = _mapper.Map<IEnumerable<SirCoPOS.Reports.Entities.PlanPago>>(venta.PlanPagos);
            var planPagosDet = _mapper.Map<IEnumerable<SirCoPOS.Reports.Entities.PlanPagoDetalle>>(venta.PlanPagosDetalle);
            var mensajes = _mapper.Map<IEnumerable<SirCoPOS.Reports.Entities.TicketMensaje>>(venta.TicketMensajes);

            var numcopias = 1;
            if (!reimpresion) {
                numcopias = _data.PrintNumCopias();
            }
            var list = new List<SirCoPOS.Reports.Entities.ReciboCompra>() { item };
            var dic = new Dictionary<string, IEnumerable<object>>() {
                { "reciboDataSet", list },
                { "productosDataSet", productos },
                { "pagosDataSet", pagos },
                { "planPagosDataSet", planPagos },
                { "planPagosDetDataSet", planPagosDet },
                { "mensajesDataSet", mensajes }
            };
            if (System.Diagnostics.Debugger.IsAttached)
            {
                for (var i = 1; i <= numcopias; i++)
                {
                    _viewer.OpenViewer(
                        fullname: "SirCoPOS.Reports.ReciboVenta.rdlc",
                        library: "SirCoPOS.Reports",
                        datasources: dic);
                }
            }
            else
            {
                for (var i=1; i <= numcopias; i++) {
                    var pd = new Helpers.PrintFile(
                    fullname: "SirCoPOS.Reports.ReciboVenta.rdlc",
                    library: "SirCoPOS.Reports",
                    datasources: dic);
                    pd.Print();
                }
            }

            if (item.ContraVale == 1)
            {
                var CVale = _proxy.GetContraVale(sucursal, folio);
                var itemc = _mapper.Map<SirCoPOS.Reports.Entities.ContraVale>(CVale.Recibo);
                var listc = new List<SirCoPOS.Reports.Entities.ContraVale>() { itemc };
                var dictc = new Dictionary<string, IEnumerable<object>>() {
                    { "ContraValeDataSet", listc },
                };

                if (System.Diagnostics.Debugger.IsAttached) {
                    _viewer.OpenViewer(
                        fullname: "SirCoPOS.Reports.ContraVale.rdlc",
                        library: "SirCoPOS.Reports",
                        datasources: dictc);
                }
                else {

                    var pdc = new Helpers.PrintFile(
                    fullname: "SirCoPOS.Reports.ContraVale.rdlc",
                    library: "SirCoPOS.Reports",
                    datasources: dictc);
                    pdc.Print();
                }
            }



        }
    public void ContraVale(string sucursal, string folio, bool reimpresion = false)
        {
            var CVale = _proxy.GetContraVale(sucursal, folio);
            var item = _mapper.Map<SirCoPOS.Reports.Entities.ContraVale>(CVale.Recibo);
            var list = new List<SirCoPOS.Reports.Entities.ContraVale>() { item };
            var dictc = new Dictionary<string, IEnumerable<object>>() {
                { "ContraValeDataSet", list },
            };

            if (System.Diagnostics.Debugger.IsAttached) { 
                _viewer.OpenViewer(
                    fullname: "SirCoPOS.Reports.ContraVale.rdlc",
                    library: "SirCoPOS.Reports",
                    datasources: dictc);
            }
            else { 
                var pdc = new Helpers.PrintFile(
                fullname: "SirCoPOS.Reports.ContraVale.rdlc",
                library: "SirCoPOS.Reports",
                datasources: dictc);
                pdc.Print();
            }
        }
        public void Devolucion(string sucursal, string folio)
        {
            var devolucion = _proxy.GetReciboDevolucion(sucursal, folio);

            var item = _mapper.Map<SirCoPOS.Reports.Entities.ReciboDevolucion>(devolucion.Recibo);
            var productos = _mapper.Map<IEnumerable<SirCoPOS.Reports.Entities.Producto>>(devolucion.Productos);
            var pagos = _mapper.Map<IEnumerable<SirCoPOS.Reports.Entities.Pago>>(devolucion.Pagos);

            var list = new List<SirCoPOS.Reports.Entities.ReciboDevolucion>() { item };
            var dic = new Dictionary<string, IEnumerable<object>>() {
                { "devolucionDataSet", list },
                { "productosDataSet", productos },
                { "pagoDataSet", pagos }
            };

            if (System.Diagnostics.Debugger.IsAttached)
            {
                _viewer.OpenViewer(
                    fullname: "SirCoPOS.Reports.ReciboDevolucion.rdlc",
                    library: "SirCoPOS.Reports",
                    datasources: dic);
            }
            else
            {
                var pd = new Helpers.PrintFile(
                fullname: "SirCoPOS.Reports.ReciboDevolucion.rdlc",
                library: "SirCoPOS.Reports",
                datasources: dic);
                pd.Print();
            }
            
        }


        
    }
}

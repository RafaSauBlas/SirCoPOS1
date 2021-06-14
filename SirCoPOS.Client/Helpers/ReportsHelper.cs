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

        public ReportsHelper()
        {
            _proxy = CommonServiceLocator.ServiceLocator.Current.GetInstance<Common.ServiceContracts.IPrintServiceAsync>();
            _mapper = CommonServiceLocator.ServiceLocator.Current.GetInstance<AutoMapper.IMapper>();
            _viewer = CommonServiceLocator.ServiceLocator.Current.GetInstance<Utilities.Interfaces.IReportViewer>();
        }

        public string EnLetras(decimal? numero)
        {
            if (numero > 0)
            {
                decimal num = Math.Round(numero.Value, 2);
                decimal dec = num % 1;
                decimal ent = num - dec;

                return "(" +enteros(ent).Trim() + " Pesos " + decimales(dec * 100) + " M.N." + ")";
            }
            else if (numero == 0)
                return "Cero";
            else
                return string.Empty;
        }

        private static string enteros(decimal numero)
        {
            int count = 0;
            decimal tmp = numero;
            while (tmp / 1000 >= 1)
            {
                count++;
                tmp /= 1000;
            }
            tmp = numero;
            decimal bloque = 0, anterior;
            string res = String.Empty;
            for (int i = count; i >= 0; i--)
            {
                anterior = bloque;
                bloque = Math.Truncate(tmp / (decimal)Math.Pow(1000, i));
                tmp = tmp % (decimal)Math.Pow(1000, i);

                if (bloque == 0)
                {
                    if (anterior > 0 && i % 2 == 0)
                        res += terminaciones(i, bloque) + " ";
                }
                else
                    res += centenas(bloque, i) + terminaciones(i, bloque) + " ";
            }

            return res;
        }

        private static string terminaciones(int num, decimal bloque)
        {
            switch (num)
            {
                case 0: return String.Empty;
                case 1:
                case 3:
                case 5:
                case 7:
                case 9: return "Mil";
                case 2: return bloque == 1 ? "Millon" : "Millones";
                case 4: return bloque == 1 ? "Billon" : "Billones";
                case 6: return bloque == 1 ? "Trillon" : "Trillones";
                case 8: return bloque == 1 ? "Cuatrillon" : "Cuatrillones";
                default: return "N/A";
            }
        }

        private static string centenas(decimal numero, int count)
        {
            int c = (int)Math.Truncate(numero / 100);
            int r = (int)(numero % 100);
            switch (c)
            {
                case 9: return r > 0 ? "Novecientos " + decenas(r, c, count) : "Novecientos ";
                case 8: return r > 0 ? "Ochocientos " + decenas(r, c, count) : "Ochocientos ";
                case 7: return r > 0 ? "Setecientos " + decenas(r, c, count) : "Setecientos ";
                case 6: return r > 0 ? "Seiscientos " + decenas(r, c, count) : "Seiscientos ";
                case 5: return r > 0 ? "Quinientos " + decenas(r, c, count) : "Quinientos ";
                case 4: return r > 0 ? "Cuatrocientos " + decenas(r, c, count) : "Cuatrocientos ";
                case 3: return r > 0 ? "Trecientos " + decenas(r, c, count) : "Trecientos ";
                case 2: return r > 0 ? "Doscientos " + decenas(r, c, count) : "Doscientos ";
                case 1: return r > 0 ? "Ciento " + decenas(r, c, count) : "Cien ";
                default: return decenas(r, c, count);
            }
        }

        private static string decenas(decimal numero, int cen, int count)
        {
            int c = (int)Math.Truncate(numero / 10);
            int r = (int)(numero % 10);
            switch (c)
            {
                case 9: return "Noventa " + (r > 0 ? "y " + unidades(r, c, cen, count) : String.Empty);
                case 8: return "Ochenta " + (r > 0 ? "y " + unidades(r, c, cen, count) : String.Empty);
                case 7: return "Setenta " + (r > 0 ? "y " + unidades(r, c, cen, count) : String.Empty);
                case 6: return "Sesenta " + (r > 0 ? "y " + unidades(r, c, cen, count) : String.Empty);
                case 5: return "Cincuenta " + (r > 0 ? "y " + unidades(r, c, cen, count) : String.Empty);
                case 4: return "Cuarenta " + (r > 0 ? "y " + unidades(r, c, cen, count) : String.Empty);
                case 3: return "Treinta " + (r > 0 ? "y " + unidades(r, c, cen, count) : String.Empty);
                case 2: return r > 0 ? "Veinti" + unidades(r, c, cen, count).ToLower() : "Veinte ";
                case 1: return r > 0 ? unidadesDiez(r) : "Diez ";
                case 0: return unidades(r, c, cen, count);
                default: return "N/A";
            }
        }

        private static string unidades(int numero, int dec, int cen, int count)
        {
            switch (numero)
            {
                case 9: return "Nueve ";
                case 8: return "Ocho ";
                case 7: return "Siete ";
                case 6: return "Seis ";
                case 5: return "Cinco ";
                case 4: return "Cuatro ";
                case 3: return "Tres ";
                case 2: return "Dos ";
                case 1:
                    return count % 2 != 0
                ? (dec >= 1 || cen >= 1 ? "Un " : String.Empty)
                : (count > 0 ? "Un " : "Uno ");
                case 0: return String.Empty;
                default: return "N/A";
            }
        }

        private static string unidadesDiez(int numero)
        {
            switch (numero)
            {
                case 9: return "Diecinueve ";
                case 8: return "Dieciocho ";
                case 7: return "Diecisiete ";
                case 6: return "Dieciseis ";
                case 5: return "Quince ";
                case 4: return "Catorce ";
                case 3: return "Trece ";
                case 2: return "Doce ";
                case 1: return "Once ";
                default: return "N/A";
            }
        }

        private static string decimales(decimal numero)
        {
            int num = (int)numero;
            return num > 99 ? "00/100" : num.ToString("00") + "/100";
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


        public void Compra(string sucursal, string folio)
        {
            var venta = _proxy.GetReciboCompra(sucursal, folio);

            var item = _mapper.Map<SirCoPOS.Reports.Entities.ReciboCompra>(venta.Recibo);
            var productos = _mapper.Map<IEnumerable<SirCoPOS.Reports.Entities.Producto>>(venta.Productos);
            var pagos = _mapper.Map<IEnumerable<SirCoPOS.Reports.Entities.Pago>>(venta.Pagos);
            var planPagos = _mapper.Map<IEnumerable<SirCoPOS.Reports.Entities.PlanPago>>(venta.PlanPagos);
            var planPagosDet = _mapper.Map<IEnumerable<SirCoPOS.Reports.Entities.PlanPagoDetalle>>(venta.PlanPagosDetalle);
            var mensajes = _mapper.Map<IEnumerable<SirCoPOS.Reports.Entities.TicketMensaje>>(venta.TicketMensajes);
            item.CantidadLetra = EnLetras(item.Efectivo).ToUpper();
            
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
                _viewer.OpenViewer(
                        fullname: "SirCoPOS.Reports.ReciboVenta.rdlc",
                        library: "SirCoPOS.Reports",
                        datasources: dic);
            }
            else
            {
                var pd = new Helpers.PrintFile(
                fullname: "SirCoPOS.Reports.ReciboVenta.rdlc",
                library: "SirCoPOS.Reports",
                datasources: dic);
                pd.Print();
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

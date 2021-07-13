using SirCoPOS.Common.Entities.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace SirCoPOS.Services
{
    public class PrintService : Common.ServiceContracts.IPrintService
    {
        public ReciboCancelacionReport GetReciboCancelacion(string sucursal, string folio)
        {
            var ctxpv = new DataAccess.SirCoPVDataContext();
            var ctxco = new DataAccess.SirCoControlDataContext();
            var ctx = new DataAccess.SirCoDataContext();

            DataAccess.SirCoPV.Venta cancela = ctxpv.Ventas.Where(i => i.sucursal == sucursal && i.venta == folio).Single();
            DataAccess.SirCoControl.Sucursal suc = ctxco.Sucursales.Where(i => i.sucursal == cancela.sucursal).Single();

            string vendedor = this.GetEmpleado(cancela.idvendedor);
            string cajero = this.GetUsuario(cancela.idusuariocancela);
            var item = new ReciboCancelacionReport
            {
                Recibo = new ReciboCancelacion
                {
                    SucursalId = cancela.sucursal,
                    SucursalNombre = suc.descrip,
                    Direccion = suc.calle,
                    Colonia = suc.colonia,
                    Folio = cancela.sucursal + "-" + cancela.venta,
                    Fecha = cancela.fumcancela.Value,
                    FechaVenta = cancela.fecha.Value,
                    SucursalVenta = cancela.sucursal,
                    FolioVenta = cancela.sucursal + "-" + cancela.venta,
                    VendedorId = $"{cancela.idvendedor}",
                    VendedorNombre = vendedor,
                    CajeroId = $"{cancela.idcajero}",
                    CajeroNombre = cajero,
                    Observaciones = cancela.motivocancela,
                }
            };

            var plist = new List<Producto>();
            foreach (var det in cancela.Detalles)
            {
                var serie = ctx.Series.Where(i => i.serie == det.serie).Single();

                plist.Add(
                    new Producto
                    {
                        Serie = det.serie,
                        Precio = det.precio.Value,
                        Importe = det.precdesc.Value,
                        Marca = det.marca,
                        Descripcion = serie.Articulo.Descripcion
                    });
            }
            item.Productos = plist;

            return item;
        }

        public ReciboContraValeReport GetContraVale(string sucursal, string folio)
        {
            var ctxpv = new DataAccess.SirCoPVDataContext();
            var ctxcr = new DataAccess.SirCoCreditoDataContext();
            var ctxco = new DataAccess.SirCoControlDataContext();

            DataAccess.SirCoCredito.ContraVale cvale = ctxcr.ContraVales.Where(i => i.sucursal == sucursal && i.referenc == folio).Single();
            DataAccess.SirCoPV.Venta venta = ctxpv.Ventas.Where(i => i.sucursal == sucursal && i.venta == folio).Single();
            DataAccess.SirCoControl.Sucursal suc = ctxco.Sucursales.Where(i => i.sucursal == venta.sucursal).Single();
            string vendedor = this.GetEmpleado(venta.idvendedor);
            string cajero = this.GetUsuario(cvale.idusuario);

            ArrayList a = this.GetCliente(venta.idcliente);
            string nombre_cliente = "";
            string direccion_cliente = "";
            string colonia_cliente = "";
            string ciudad_cliente = "";
            string estado_cliente = "";
            string cp_cliente = "";
            if (a != null && a.Count > 0)
            {
                nombre_cliente = (string)a[0];
                direccion_cliente = (string)a[1];
                colonia_cliente = (string)a[2];
                ciudad_cliente = (string)a[3];
                estado_cliente = (string)a[4];
                cp_cliente = (string)a[5];
            }

            var item = new ReciboContraValeReport
            {
                Recibo = new ReciboContraVale
                {
                    Folio = cvale.sucursal + "-" + cvale.cvale,
                    Fecha = cvale.fum.Value,
                    Nota = cvale.sucursal + "-" + cvale.referenc,
                    NotaFecha = venta.fum.Value,
                    Vencimiento = (DateTime)cvale.caduca,
                    ValorMaximo = (decimal)cvale.saldo,
                    RFC = "CTO-911211JL9",
                    SucId = venta.sucursal,
                    SucNombre = suc.descrip,
                    SucDireccion = suc.calle,
                    SucColonia = suc.colonia,
                    SucCiudad = suc.ciudad,
                    SucEstado = suc.estado,
                    SucCodPostal = suc.codpostal,
                    VendedorId = $"{venta.idvendedor}",
                    VendedorNombre = vendedor,
                    CajeroId = $"{cvale.idusuario}",
                    CajeroNombre = cajero,
                    ClienteId = cvale.cliente,
                    ClienteSuc = cvale.succte,
                    ClienteNombre = nombre_cliente,
                    ClienteDireccion = direccion_cliente,
                    ClienteColonia = colonia_cliente,
                    ClienteCiudad = ciudad_cliente,
                    ClienteEstado = estado_cliente,
                    ClienteCodPostal = cp_cliente,
                    Distribuidor = cvale.distrib,
                    DistribuidorNombre = this.GetDistrib(cvale.distrib)

                }
            };

            return item;
        }
    

public ReciboDevolucionReport GetReciboDevolucion(string sucursal, string folio)
        {
            var ctxpv = new DataAccess.SirCoPVDataContext();
            var ctxc = new DataAccess.SirCoControlDataContext();
            var ctx = new DataAccess.SirCoDataContext();
            
            var devolucion = ctxpv.Devoluciones.Where(i => i.sucursal == sucursal && i.devolvta == folio).Single();

            
            string vendedor = this.GetEmpleado(devolucion.idvendedor);
            string cajero = this.GetUsuario(devolucion.idcajero);
            var suc = ctxc.Sucursales.Where(i => i.sucursal == devolucion.sucursal).Single();
            var ventaSucursal = devolucion.referencia.Substring(0, 2);
            var ventaFolio = devolucion.referencia.Substring(2);
            DataAccess.SirCoPV.Venta venta = ctxpv.Ventas.Where(i => i.sucursal == ventaSucursal && i.venta == ventaFolio).Single();

            var item = new ReciboDevolucionReport
            { 
                Recibo = new ReciboDevolucion
                {
                    SucursalId = devolucion.sucursal,
                    SucursalNombre = suc.descrip,
                    Direccion = suc.calle,
                    Colonia = suc.colonia,
                    Fecha = devolucion.fum.Value,
                    VendedorId = $"{devolucion.idvendedor}",
                    VendedorNombre = vendedor,
                    CajeroId = $"{devolucion.idcajero}",
                    CajeroNombre = cajero,
                    Folio = devolucion.devolvta,
                    SucursalVenta = ventaSucursal,
                    FolioVenta = ventaFolio,
                    Disponible = devolucion.disponible,
                }
            };
            var plist = new List<Producto>();
            foreach (var det in devolucion.Detalles)
            {
                var serie = ctx.Series.Where(i => i.serie == det.serie).Single();

                plist.Add(
                    new Producto { 
                        Serie = det.serie, 
                        Precio = det.precio.Value, 
                        Importe = det.precdesc.Value, 
                        Marca = det.marca, 
                        Descripcion = serie.Articulo.Descripcion
                    });
            }            
            item.Productos = plist;

            List<Pago> pdlist = new List<Common.Entities.Reports.Pago>();
            var pagos = venta.Pago.Detalle;
            foreach (var det in pagos)
            {
                Pago pa = new Pago
                {
                    FormaPago = ((Common.Constants.FormaPago)det.idformapago).ToString(),
                    Importe = det.importe.Value,
                    Referencia = null,
                    Folio = null
                };
                pdlist.Add(pa);
            }
            item.Pagos = pdlist;
            return item;
        }

        
        public ReciboCompraReport GetReciboCompra(string sucursal, string folio)
        {
            var ctxpv = new DataAccess.SirCoPVDataContext();
            var ctxc = new DataAccess.SirCoControlDataContext();
            var ctxn = new DataAccess.SirCoNominaDataContext();
            var ctx = new DataAccess.SirCoDataContext();
            var ctxa = new DataAccess.SirCoDataContext();

            DataAccess.SirCoPV.Venta venta = ctxpv.Ventas.Where(i => i.sucursal == sucursal && i.venta == folio).Single();
            DataAccess.SirCo.DineroDetalle dinero = ctxa.DinerosDetalle.Where(i => i.sucnota == sucursal && i.nota == folio).SingleOrDefault();

            string vendedor = this.GetEmpleado(venta.idvendedor);
            string cajero = this.GetUsuario(venta.idcajero);
            ArrayList a = this.GetCliente(venta.idcliente);
            string nombcliente = "";
            string callecliente = "";
            string coloniacliente = "";
            string cdcliente = "";
            string telcliente = "";
            if (a != null && a.Count >0) { 
                nombcliente = (string)a[0];
                callecliente = (string)a[1];
                if (a[2].ToString().Length > 0)
                    coloniacliente = (string)a[2];
                if (a[3].ToString().Length > 0)
                    cdcliente = (string)a[3] + ", " + (string)a[4] + " C.P. " +(string)a[5];
                if (a[6].ToString().Length > 0)
                    telcliente = (string)a[6];
            }
            
            DataAccess.SirCoControl.Sucursal suc = ctxc.Sucursales.Where(i => i.sucursal == venta.sucursal).Single();

            var ctxcr = new DataAccess.SirCoCreditoDataContext();
            var plan = ctxcr.PlanPagos.Where(i => i.sucursal == venta.sucursal && i.nota == venta.venta);

            string cli_plan = "";
            string distrib = "";
            string nota = "";
            string vale = "";
            int numpagos = 0;
            string distribuidor = "";
            decimal? blindaje = 0;
            decimal? saldo = 0;

            if (plan.Any() ){  
                cli_plan = plan.Single().sucursal + "-" + plan.SingleOrDefault().cliente;
                distrib = plan.Single().distrib;
                distribuidor = this.GetDistrib(distrib);
                nota = plan.Single().sucursal + "-" + plan.Single().nota;
                vale = plan.Single().negocio + "-" + plan.Single().vale;
                numpagos = (int)plan.Single().pagos;
                blindaje = (decimal)plan.Single().blindaje;
                saldo = plan.Single().saldo;
            }
            short? contravale = 0;
            if (distrib != "")
            {

                //contravale = ctxcr.Distribuidores.Where(i => i.distrib == distrib).SingleOrDefault().contravale;
                var cvale = ctxcr.ContraVales.Where(i => (i.referenc == venta.venta) && (i.sucursal == sucursal));
                if (cvale.Any())
                {
                    contravale = 1;
                }
            }

            ReciboCompraReport item = new ReciboCompraReport
            {
                Recibo = new Common.Entities.Reports.ReciboCompra
                {
                    RFC = "CTO-911211JL9",
                    SucursalId = venta.sucursal,
                    SucursalNombre = suc.descrip,
                    Direccion = suc.calle,
                    Colonia = suc.colonia,
                    Ciudad = suc.ciudad,
                    Estado = suc.estado,
                    CP = suc.codpostal,
                    VendedorId = $"{venta.idvendedor}",
                    VendedorNombre = vendedor,
                    CajeroId = $"{venta.idcajero}",
                    CajeroNombre = cajero,
                    Folio = venta.venta,
                    Efectivo = 3975,
                    CantidadLetra = "",
                    Descuento = 500,
                    Blindaje = blindaje,
                    Cambio = DateTime.Parse("2019-07-13"),
                    Fecha = venta.fum.Value,
                    Dinero = dinero?.importe,
                    ClienteId = cli_plan,
                    ClienteNombre = nombcliente,
                    ClienteCalle = callecliente,
                    ClienteColonia = coloniacliente,
                    ClienteCiudad = cdcliente,
                    ClienteTelefono = telcliente,
                    Distrib = distrib,
                    DistribNombre = distribuidor,
                    Nota = nota,
                    Vale = vale,
                    NumPagos = numpagos,
                    ImporteSaldo = saldo,
                    NotaFecha = venta.fum.Value,
                    ContraVale = contravale,
                }
            };

            decimal desc = 0;
            decimal aPagar = 0;
            var plist = new List<Common.Entities.Reports.Producto>();
            foreach (var det in venta.Detalles)
            {
                var serie = ctx.Series.Where(i => i.serie == det.serie).Single();

                plist.Add(
                    new Common.Entities.Reports.Producto { 
                        Serie = det.serie, 
                        Precio = det.precio.Value, 
                        Importe = det.precdesc.Value, 
                        Marca = det.marca, 
                        Descripcion = serie.Articulo.Descripcion
                    });
                desc += det.precio.Value - det.precdesc.Value;
                aPagar += det.precdesc.Value;
            }
            item.Recibo.CantidadLetra = EnLetras(aPagar + item.Recibo.Blindaje).ToUpper();
            item.Productos = plist;
            item.Recibo.Descuento = desc;

            decimal importe = 0;
            List<Pago> pdlist = new List<Common.Entities.Reports.Pago>();
            var pagos = venta.Pago.Detalle;
            decimal? PagarCon = 0;
            foreach (var det in pagos)
            {
                Pago pa = new Pago
                {
                    FormaPago = ((Common.Constants.FormaPago)det.idformapago).ToString(),
                    Importe = det.importe.Value, 
                    Efectivo = (decimal)det.efectivo,
                    Referencia = null,
                    Folio = null
                };
                pdlist.Add(pa);

                if (((Common.Constants.FormaPago)det.idformapago) == Common.Constants.FormaPago.EF) {
                    PagarCon = det.efectivo;
                    importe = (decimal)det.efectivo - (decimal)det.importe;
                }
                
            }
            item.Pagos = pdlist;
            item.Recibo.PagarCon = PagarCon;
            item.Recibo.Efectivo = importe;
            
            //var ctxcr = new DataAccess.SirCoCreditoDataContext();
            //var plan = ctxcr.PlanPagos.Where(i => i.sucursal == venta.sucursal && i.nota == venta.venta);

            decimal pago1 = 0;
            decimal pago2 = 0;
            DateTime fecha1 = default(DateTime);

            DataAccess.SirCoCredito.PlanPagos planp = ctxcr.PlanPagos.Where(i => i.sucursal == sucursal && i.nota == venta.venta).SingleOrDefault();
            List<PlanPagoDetalle> pplist = new List<Common.Entities.Reports.PlanPagoDetalle>();
            if (planp != null ) { 
                foreach (var det in planp.Detalle)
                {
                    pplist.Add(
                        new Common.Entities.Reports.PlanPagoDetalle
                        {
                            Pago = (int)det.pago,
                            FechaAplicar = det.fechaaplicar,
                            Importe = det.importe
                        });
                    if (det.pago == 1)
                    {
                        pago1 = det.importe;
                        fecha1 = det.fechaaplicar.AddDays(-2);
                    }
                    if (det.pago == 2)
                    {
                        pago2 = det.importe;
                    }
                }
            }
            item.PlanPagosDetalle = pplist;
            item.Recibo.Fecha1erPago = fecha1;
            item.Recibo.ImportePrimerPago = pago1;
            item.Recibo.ImporteSigPagos = pago2;

            var mlist = new List<Common.Entities.Reports.TicketMensaje>();
            var msg = ctxc.TicketMsjs.OrderBy(c => c.Orden);
            foreach ( var ren in msg ) {
                mlist.Add(new TicketMensaje
                {
                    Orden = ren.Orden,
                    Renglon = ren.Renglon
                });
            }
            item.TicketMensajes = mlist;

            return item;
        }

        private string GetEmpleado(int? id)
        {
            var ctxn = new DataAccess.SirCoNominaDataContext();

            if (!id.HasValue)
                return null;

            var emp = ctxn.Empleados.Where(i => i.idempleado == id).SingleOrDefault();
            if (emp == null)
                return null;

            return $"{emp.nombre.ToUpper()} {emp.appaterno.ToUpper()} {emp.apmaterno.ToUpper()}";
        }
        private string GetColonia(int? id)
        {
            var ctxn = new DataAccess.SirCoControlDataContext();

            if (!id.HasValue)
                return null;

            var col = ctxn.Colonias.Where(i => i.idcolonia == id).SingleOrDefault();
            if (col == null)
                return "";

            return $"{col.colonia.ToUpper()}";
        }
        private string GetCiudad(int? id)
        {
            var ctxc = new DataAccess.SirCoControlDataContext();

            if (!id.HasValue)
                return null;

            var cd = ctxc.Ciudades.Where(i => i.idciudad == id).SingleOrDefault();
            if (cd == null)
                return "";

            return $"{cd.ciudad.ToUpper()}";
        }
        private string GetEstado(int? id)
        {
            var ctxc = new DataAccess.SirCoControlDataContext();

            if (!id.HasValue)
                return null;

            var es = ctxc.Estados.Where(i => i.idestado == id).SingleOrDefault();
            if (es == null)
                return "";

            return $"{es.abrev.ToUpper()}";
        }
        private string GetUsuario(int? id)
        {
            var ctxn = new DataAccess.SirCoNominaDataContext();

            if (!id.HasValue)
                return null;

            var emp = ctxn.Empleados.Where(i => i.idempleado == id).SingleOrDefault();
            if (emp == null)
                return null;

            return $"{emp.usuariosistema.ToUpper()}";
        }

        private ArrayList GetCliente(int? id)
        {
            var ctxr = new DataAccess.SirCoCreditoDataContext();
            ArrayList Cliente = new ArrayList();

            if (!id.HasValue)
                return null;

            var cli = ctxr.Clientes.Where(i => i.idcliente == id).SingleOrDefault();
            if (cli == null)
                return Cliente;
                
            Cliente.Add($"{cli.nombrecompleto.ToUpper()}");
            Cliente.Add($"{cli.calle.ToUpper()}");
            Cliente.Add(GetColonia(cli.idcolonia));
            Cliente.Add(GetCiudad(cli.idciudad));
            Cliente.Add(GetEstado(cli.idestado));
            Cliente.Add(cli.codigopostal);
            Cliente.Add($"{cli.celular1}");

            return Cliente;
        }


        private string GetDistrib(string distrib)
        {
            var ctxr = new DataAccess.SirCoCreditoDataContext();
            var dist = ctxr.Distribuidores.Where(i => i.distrib == distrib).SingleOrDefault();
            if (dist == null)
                return "";

            return $"{dist.nombrecompleto.ToUpper()}";
        }

        public string EnLetras(decimal? numero)
        {
            if (numero > 0)
            {
                decimal num = Math.Round(numero.Value, 2);
                decimal dec = num % 1;
                decimal ent = num - dec;

                return "(" + enteros(ent).Trim() + " Pesos " + decimales(dec * 100) + " M.N." + ")";
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


    }
}

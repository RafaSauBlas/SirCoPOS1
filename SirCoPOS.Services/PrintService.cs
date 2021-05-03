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
            DataAccess.SirCoPV.Venta venta = ctxpv.Ventas.Where(i => i.sucursal == sucursal && i.venta == ventaFolio).Single();

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
                    FolioVenta = ventaFolio
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
            var ctxa = new DataAccess.SirCoAPPDataContext();

            DataAccess.SirCoPV.Venta venta = ctxpv.Ventas.Where(i => i.sucursal == sucursal && i.venta == folio).Single();
            DataAccess.SirCoAPP.DineroDetalle dinero = ctxa.DinerosDetalle.Where(i => i.sucnota == sucursal && i.nota == folio).SingleOrDefault();

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
                coloniacliente = (string)a[2];
                cdcliente = (string)a[3] + ", " + (string)a[4] + " C.P. " +(string)a[5];
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
                contravale = ctxcr.Distribuidores.Where(i => i.distrib == distrib).SingleOrDefault().contravale;
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
            decimal importe = 0;
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
                importe += det.precdesc.Value;
            }                        
            item.Productos = plist;
            item.Recibo.Descuento = desc;
            item.Recibo.Efectivo = importe;

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
                return null;

            return $"{col.colonia.ToUpper()}";
        }
        private string GetCiudad(int? id)
        {
            var ctxc = new DataAccess.SirCoControlDataContext();

            if (!id.HasValue)
                return null;

            var cd = ctxc.Ciudades.Where(i => i.idciudad == id).SingleOrDefault();
            if (cd == null)
                return null;

            return $"{cd.ciudad.ToUpper()}";
        }
        private string GetEstado(int? id)
        {
            var ctxc = new DataAccess.SirCoControlDataContext();

            if (!id.HasValue)
                return null;

            var es = ctxc.Estados.Where(i => i.idestado == id).SingleOrDefault();
            if (es == null)
                return null;

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

    }
}

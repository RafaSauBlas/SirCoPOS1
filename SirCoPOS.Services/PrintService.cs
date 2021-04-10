using SirCoPOS.Common.Entities.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Services
{
    public class PrintService : Common.ServiceContracts.IPrintService
    {
        public ReciboDevolucionReport GetReciboDevolucion(string sucursal, string folio)
        {
            var ctxpv = new DataAccess.SirCoPVDataContext();
            var ctxc = new DataAccess.SirCoControlDataContext();
            var ctx = new DataAccess.SirCoDataContext();

            var devolucion = ctxpv.Devoluciones.Where(i => i.sucursal == sucursal && i.devolvta == folio).Single();

            var vendedor = this.GetEmpleado(devolucion.idvendedor);
            var cajero = this.GetEmpleado(devolucion.idcajero);

            var suc = ctxc.Sucursales.Where(i => i.sucursal == devolucion.sucursal).Single();
            var ventaSucursal = devolucion.referencia.Substring(0, 2);
            var ventaFolio = devolucion.referencia.Substring(2);

            var item = new ReciboDevolucionReport
            { 
                Recibo = new ReciboDevolucion
                {
                    SucursalId = devolucion.sucursal,
                    SucursalNombre = suc.descrip,
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
                
            return $"{emp.nombre} {emp.appaterno} {emp.apmaterno}";
        }

        public ReciboCompraReport GetReciboCompra(string sucursal, string folio)
        {
            var ctxpv = new DataAccess.SirCoPVDataContext();
            var ctxc = new DataAccess.SirCoControlDataContext();
            var ctxn = new DataAccess.SirCoNominaDataContext();
            var ctx = new DataAccess.SirCoDataContext();
            var ctxa = new DataAccess.SirCoAPPDataContext();

            var venta = ctxpv.Ventas.Where(i => i.sucursal == sucursal && i.venta == folio).Single();
            var dinero = ctxa.DinerosDetalle.Where(i => i.sucnota == sucursal && i.nota == folio).SingleOrDefault();

            var vendedor = this.GetEmpleado(venta.idvendedor);
            var cajero = this.GetEmpleado(venta.idcajero);

            var suc = ctxc.Sucursales.Where(i => i.sucursal == venta.sucursal).Single();

            var item = new ReciboCompraReport
            {
                Recibo = new Common.Entities.Reports.ReciboCompra
                {
                    SucursalId = venta.sucursal,
                    SucursalNombre = suc.descrip,
                    RFC = "CTO-911211JL9",
                    Direccion = suc.calle,
                    Colonia = suc.colonia,
                    Fecha = venta.fum.Value,
                    VendedorId = $"{venta.idvendedor}",
                    VendedorNombre = vendedor,                    
                    CajeroId = $"{venta.idcajero}",
                    CajeroNombre = cajero,
                    Folio = venta.venta,
                    Efectivo = 3975,
                    Descuento = 500,
                    Cambio = DateTime.Parse("2019-07-13"),
                    Dinero = dinero?.importe
                }
            };

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
            }                        
            item.Productos = plist;

            var pdlist = new List<Common.Entities.Reports.Pago>();
            //if(!ctxpv.Entry(venta).Reference(i => i.Pago).IsLoaded)
            //    ctxpv.Entry(venta).Reference(i => i.Pago).Load();
            //if (!ctxpv.Entry(venta.Pago).Collection(i => i.Detalle).IsLoaded)
            //    ctxpv.Entry(venta.Pago).Collection(i => i.Detalle).Load();

            var pagos = venta.Pago.Detalle;
            foreach (var det in pagos)
            {
                var pa = new Pago
                {
                    FormaPago = ((Common.Constants.FormaPago)det.idformapago).ToString(),
                    Importe = det.importe.Value, 
                    Referencia = null,
                    Folio = null
                };
                pdlist.Add(pa);
            }
            item.Pagos = pdlist;

            var pplist = new List<Common.Entities.Reports.PlanPago>();
            var ctxcr = new DataAccess.SirCoCreditoDataContext();
            //==============================================================================================
            var ppagos = ctxcr.PlanPagosDetalle.Where(i => i.sucursal == "447279");
            foreach (var det in ppagos)
            {
                pplist.Add(new PlanPago
                {
                    Pago = (int)det.pago, 
                    Date = det.fechaaplicar, 
                    Importe = det.importe
                });
            }
            item.PlanPagos = pplist;

            return item;
        }
    }
}

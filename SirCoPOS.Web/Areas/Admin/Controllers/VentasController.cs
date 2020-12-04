using SirCoPOS.Web.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SirCoPOS.Web.Areas.Admin.Controllers
{
    [Authorize]
    public class VentasController : Controller
    {
        // GET: Admin/Ventas
        public ActionResult Index(int? fondo)
        {
            var ctx = new DataAccess.SirCoPVDataContext();
            if (fondo.HasValue)
            {
                var ctxpos = new DataAccess.SirCoPOSDataContext();
                var f = ctxpos.Fondos.Where(i => i.Id == fondo).SingleOrDefault();
                if (f == null)
                    return HttpNotFound();
                var q = ctx.Ventas.Where(i => i.fum > f.FechaApertura 
                    && i.idcajero == f.ResponsableId
                    && i.sucursal == f.CajaSucursal);
                if (f.FechaCierre.HasValue)
                    q = q.Where(i => i.fum < f.FechaCierre);
                var res = q.OrderByDescending(i => i.fum);
                return View(q);
            }
            else
            {
                var date = DateTime.Today.AddDays(-15);                
                var q = ctx.Ventas.Where(i => i.fecha > date).OrderByDescending(i => i.fum).Take(50);
                return View(q);
            }            
        }
        private Models.Persona GetEmpleado(int? id)
        {
            var ctxn = new DataAccess.SirCoNominaDataContext();

            if (!id.HasValue)
                return null;

            var emp = ctxn.Empleados.Where(i => i.idempleado == id).SingleOrDefault();
            if (emp == null)
                return new Models.Persona { Id = id.Value };

            return new Models.Persona
            {
                Id = emp.idempleado, 
                Nombre = emp.nombre, 
                ApPaterno = emp.appaterno, 
                ApMaterno = emp.apmaterno
            };            
        }
        public ActionResult Detalle(string sucursal, string folio)
        {
            var ctx = new DataAccess.SirCoPVDataContext();
            var ctxc = new DataAccess.SirCoCreditoDataContext();
            var item = ctx.Ventas.Where(i => i.sucursal == sucursal && i.venta == folio).SingleOrDefault();
            if (item == null)
                return HttpNotFound();

            Persona cliente = null;
            if (item.idcliente.HasValue)
            {
                var cli = ctxc.Clientes.Where(i => i.idcliente == item.idcliente).SingleOrDefault();
                if (cli == null)
                    cliente = new Persona { Id = item.idcliente.Value };
                else
                {
                    cliente = new Persona
                    {
                        Id = cli.idcliente, 
                        Nombre = cli.nombre, 
                        ApPaterno = cli.appaterno, 
                        ApMaterno = cli.apmaterno
                    };
                }                
            }

            var model = new Models.Venta
            {
                Sucursal = item.sucursal,
                Folio = item.venta,
                Fecha = item.fum,
                Estatus = item.estatus,
                Cajero = this.GetEmpleado(item.idcajero),
                Vendedor = this.GetEmpleado(item.idvendedor),
                Cliente = cliente
            };
            var plist = new List<Models.Producto>();
            foreach (var i in item.Detalles)
            {
                if(i.idrazon.HasValue)
                    ctx.Entry(i).Reference(k => k.NotaRazon).Load();
                plist.Add(new Models.Producto
                {
                    Renglon = i.renglon,
                    Marca = i.marca,
                    Modelo = i.estilon,
                    Serie = i.serie,
                    Precio = i.precio,
                    Pago = i.precdesc,
                    Comments = i.descuentoespecialdesc,
                    Notas = i.notas,
                    NotaRazon = i.idrazon.HasValue ? i.NotaRazon.descripcion : null
                });
            }            
            model.Detalle = plist;

            var ctxi = new DataAccess.SirCoImgDataContext();
            foreach (var det in model.Detalle)
            {
                var q = ctxi.Imagenes.Where(i => i.Marca == det.Marca && i.Estilon == det.Modelo);
                det.HasImage = q.Any();
            }

            model.ContraVales = ctxc.ContraVales.Where(i => i.sucursal == item.sucursal && i.referenc == item.venta)
                .Select(i => new Models.ContraVale { Folio = i.cvale, Disponible = i.saldo, Importe = i.importe });

            model.Devoluciones = ctx.Devoluciones.Where(i => i.referencia == item.sucursal + item.venta)
                .Select(i => new Models.DevolucionVenta { 
                    Sucursal = i.sucursal,
                    Folio = i.devolvta,
                    Importe = i.disponible
                });

            model.Pago = new Models.Pago
            {
                Fecha = item.Pago.fum,
                Estatus = item.Pago.estatus
            };
            var pagos = new List<Models.PagoDetalle>();
            foreach (var i in item.Pago.Detalle)
            {
                var pd = new Models.PagoDetalle
                {
                    FormaPago = i.idformapago,
                    Importe = i.importe
                };
                if (pd.FormaPago == (int)Common.Constants.FormaPago.DV
                    && i.referencia != null)
                {
                    pd.Sucursal = i.referencia.Substring(0, 2);
                    pd.Folio = i.referencia.Substring(2);
                }
                else if ((pd.FormaPago == (int)Common.Constants.FormaPago.TC || pd.FormaPago == (int)Common.Constants.FormaPago.TD)
                    && i.terminacion != null)
                {
                    pd.Terminacion = i.terminacion;
                    pd.Transaccion = i.transaccion;
                } 
                else if (pd.FormaPago == (int)Common.Constants.FormaPago.VA)
                {
                    pd.Folio = i.vale;
                    var qpp = ctxc.PlanPagos.Where(k => k.sucursal == sucursal && k.nota == folio && k.vale == i.vale);
                    if (qpp.Any())
                    {
                        ViewBag.hasPlanPagos = true;
                        var list = new List<Models.PlanPago>();
                        model.PlanPago = list;
                        foreach (var pp in qpp)
                        {
                            //pd.Sucursal = pp.sucursal;
                            //pd.Folio = pp.vale;

                            list.Add(new Models.PlanPago
                            {
                                Vale = pp.vale,
                                Detalle = pp.Detalle.Select(k => new Models.PlanPagoDetalle
                                {
                                    Fecha = k.fechaaplicar,
                                    Importe = k.importe,
                                    Number = k.pago
                                })
                            });
                        }
                    }
                }
                pagos.Add(pd);
            }
            model.Pago.Detalle = pagos;

            return View(model);
        }
    }
}
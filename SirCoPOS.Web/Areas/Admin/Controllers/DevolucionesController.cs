using SirCoPOS.Web.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SirCoPOS.Web.Areas.Admin.Controllers
{
    [Authorize]
    public class DevolucionesController : Controller
    {
        // GET: Admin/Devoluciones
        public ActionResult Index()
        {
            var date = DateTime.Today.AddDays(-15);
            var ctx = new DataAccess.SirCoPVDataContext();
            var q = ctx.Devoluciones.Where(i => i.fecha > date).OrderByDescending(i => i.fum).Take(50);
            return View(q);
        }
        public ActionResult Detalle(string sucursal, string folio)
        {
            var ctx = new DataAccess.SirCoPVDataContext();
            var ctxc = new DataAccess.SirCoCreditoDataContext();
            var item = ctx.Devoluciones.Where(i => i.sucursal == sucursal && i.devolvta == folio).SingleOrDefault();
            if (item == null)
                return HttpNotFound();

            var model = new Models.Devolucion
            {
                Sucursal = item.sucursal,
                Folio = item.devolvta,
                Fecha = item.fecha,
                Estatus = item.estatus,
                Disponible = item.disponible,
                Total = item.Detalles.Sum(i => i.precdesc)
            };
            model.VentaSucursal = item.referencia.Substring(0, 2);
            model.VentaFolio = item.referencia.Substring(2);

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
            model.Cliente = cliente;

            var plist = new List<Models.Producto>();
            foreach (var i in item.Detalles)
            {
                if (i.idrazon.HasValue)
                    ctx.Entry(i).Reference(k => k.Razon).Load();

                plist.Add(new Models.Producto
                {
                    Renglon = i.renglon,
                    Marca = i.marca,
                    Modelo = i.estilon,
                    Serie = i.serie,
                    Precio = i.precio,
                    Pago = i.precdesc,

                    Notas = i.notas,
                    NotaRazon = i.idrazon.HasValue ? i.Razon.descripcion : null
                });
            }            
            model.Detalle = plist;

            var ctxi = new DataAccess.SirCoImgDataContext();
            foreach (var det in model.Detalle)
            {
                var q = ctxi.Imagenes.Where(i => i.Marca == det.Marca && i.Estilon == det.Modelo);
                det.HasImage = q.Any();
            }

            var dev = $"{model.Sucursal}{model.Folio}";
            var qpd = ctx.PagosDetalle.Where(i => i.idformapago == (int)Common.Constants.FormaPago.DV
                && i.referencia == dev);
            model.Ventas = qpd.Select(i => new Models.DevolucionVenta 
            {
                Sucursal = i.sucursal,
                Folio = i.pago,
                Importe = i.importe
            });
            return View(model);
        }
    }
}
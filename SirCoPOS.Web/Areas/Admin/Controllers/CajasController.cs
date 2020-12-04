using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SirCoPOS.Web.Areas.Admin.Controllers
{
    public class CajasController : Controller
    {
        // GET: Admin/Fondos
        public ActionResult Index()
        {
            var admin = new BusinessLogic.Admin();
            var ctx = new DataAccess.SirCoPOSDataContext();
            var ctxpv = new DataAccess.SirCoPVDataContext();
            var items = ctx.Cajas
                .OrderByDescending(i => i.Disponible)
                .ThenBy(i => i.Numero)
                .Select(i => new Models.Caja
                { 
                    Item = i
                }).ToArray();
            foreach (var item in items)
            {
                var fondo = ctx.Fondos.Where(i => i.CajaSucursal == item.Item.Sucursal && i.CajaNumero == item.Item.Numero
                    && !i.FechaCierre.HasValue).SingleOrDefault();
                item.Fondo = fondo?.Id;
                item.Responsable = fondo?.ResponsableId;
            }
            //foreach (var item in items.Where(i => i.Item.Tipo == Common.Constants.TipoFondo.Cajon))
            //{
            //    item.Ventas = admin.VentasEfectivo(item.Item.Sucursal, item.Item.Numero);                
            //}
            return View(items);
        }
        public ActionResult Detalle(string sucursal, byte? numero)
        {
            var ctx = new DataAccess.SirCoPOSDataContext();
            var item = ctx.Cajas.Where(i => i.Sucursal == sucursal && i.Numero == numero).SingleOrDefault();
            if (item == null)
                return HttpNotFound();
            return View(item);
        }
    }
}
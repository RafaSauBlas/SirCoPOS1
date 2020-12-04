using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SirCoPOS.Web.Areas.Admin.Controllers
{
    public class FondosController : Controller
    {
        // GET: Admin/Fondos
        public ActionResult Index(string sucursal, byte? numero)
        {
            var ctx = new DataAccess.SirCoPOSDataContext();
            if (numero.HasValue)
            {
                var q = ctx.Fondos.Where(i => i.CajaSucursal == sucursal && i.CajaNumero == numero)
                    .OrderByDescending(i => i.FechaApertura);
                return View(q);
            }
            else
            {
                var q = ctx.Fondos.Where(i => !i.FechaCierre.HasValue)
                        .OrderByDescending(i => i.FechaApertura);
                return View(q);
            }
        }
        public ActionResult FondoFormasPago(int id)
        {
            var ctx = new DataAccess.SirCoPOSDataContext();
            var fondo = ctx.Fondos.Where(i => i.Id == id).SingleOrDefault();
            if (fondo == null)
                return HttpNotFound();
            var model = fondo.FormasPago;
            return View(model);
        }
    }
}
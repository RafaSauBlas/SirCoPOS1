using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SirCoPOS.Web.Areas.Admin.Controllers
{
    public class FondoMovimientosController : Controller
    {
        // GET: Admin/FondoMovimientos
        public ActionResult Index(int id)
        {
            var ctx = new DataAccess.SirCoPOSDataContext();
            var q = ctx.FondoMovimientos.Where(i => i.FondoId == id)
                .OrderByDescending(i => i.Fecha);
            return View(q);
        }
    }
}
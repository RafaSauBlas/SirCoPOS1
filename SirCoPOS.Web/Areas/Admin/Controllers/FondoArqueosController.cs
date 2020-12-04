using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SirCoPOS.Web.Areas.Admin.Controllers
{
    public class FondoArqueosController : Controller
    {
        // GET: Admin/FondoArqueos
        public ActionResult Index(int id)
        {
            var ctx = new DataAccess.SirCoPOSDataContext();
            var q = ctx.FondoArqueos.Where(i => i.FondoId == id).OrderByDescending(i => i.Fecha);
            return View(q);
        }
        public ActionResult FormasPago(int id)
        {
            var ctx = new DataAccess.SirCoPOSDataContext();
            var q = ctx.FondoArqueoFormaPagos.Where(i => i.FondoArqueoId == id);
            return View(q);
        }
    }
}
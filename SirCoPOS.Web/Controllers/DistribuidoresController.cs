using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SirCoPOS.Web.Controllers
{
    public class DistribuidoresController : Controller
    {
        // GET: Distribuidores
        public ActionResult Index(string id)
        {
            var ds = new Services.DataService();
            var dis = ds.FindDistribuidor(id);
            if (dis == null)
                return HttpNotFound();
            var ts = DateTime.Now - dis.Distribuidor.Date.Value;
            ViewBag.time = ts;
            return View(dis);
        }
    }
}
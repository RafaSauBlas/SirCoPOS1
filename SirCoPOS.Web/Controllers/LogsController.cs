using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using NLog;

namespace SirCoPOS.Web.Controllers
{
    public class LogsController : Controller
    {
        private readonly ILogger _log;
        public LogsController(ILogger log)
        {
            _log = log;
        }
        public ActionResult Index(int id = 50)
        {
            var ctx = new DataAccess.SirCoLogsDataContext();
            var q = ctx.Logs.OrderByDescending(i => i.Date).Take(id);
            return View(q);
        }
        public ActionResult Detail(int id)
        {
            var ctx = new DataAccess.SirCoLogsDataContext();
            var item = ctx.Logs.Where(i => i.Id == id).SingleOrDefault();
            if (item == null)
                return HttpNotFound();
            return View(item);
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Log()
        {
            var req = Request.InputStream;
            req.Seek(0, SeekOrigin.Begin);
            var json = new StreamReader(req).ReadToEnd();
            var request = JsonConvert.DeserializeObject<Models.LogRequest>(json);

            var ctx = new DataAccess.SirCoLogsDataContext();
            ctx.AddLog(
                application: request.Application ?? "Client",
                machineName: request.MachineName,
                level: request.Level,
                logger: request.Logger,
                module: "Client",
                type: request.Type,
                callsite: request.Callsite,
                message: request.Message,
                exception: request.Exception);
            return Content("OK");
        }

        public ActionResult Clear()
        {
            var ctx = new DataAccess.SirCoLogsDataContext();
            ctx.ClearLog();
            return RedirectToAction(nameof(this.Index));
        }
    }
}
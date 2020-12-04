using SirCoPOS.BusinessLogic;
using SirCoPOS.Common.Entities;
using SirCoPOS.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZXing.QrCode.Internal;

namespace SirCoPOS.Web.Controllers
{
    [Authorize]
    public class SolicitudesController : Controller
    {
        public ActionResult Credito(Guid id)
        {
            var ctx = new DataAccess.SirCoPOSDataContext();
            var cctx = new DataAccess.SirCoCreditoDataContext();
            var ctxc = new DataAccess.SirCoControlDataContext();

            var item = ctx.SolicitudesCreditoVales.Where(i => i.Id == id).SingleOrDefault();
            if (item == null)
                return HttpNotFound();

            var ditem = cctx.Distribuidores.Where(i => i.iddistrib == item.ValeCliente.iddistrib).Single();
            
            //var usado = ctx.PlanPagos.Where(i => i.vale == vale).Sum(i => i.saldo);
            //var disponible = Math.Min(item.limitevale.Value, item.disponible.Value) - usado;

            var model = new Models.Distribuidor
            {
                Id = ditem.iddistrib,
                Cuenta = ditem.distrib,
                Nombre = ditem.nombre,
                ApMaterno = ditem.appaterno,
                ApPaterno = ditem.apmaterno,
                Status = ditem.idestatus.Value,
                Electronica = ditem.solocalzado == 0,
                Plazos = ditem.idperiodicidad.Value,
                Promocion = ditem.promocion == 1,
                Disponible = ditem.disponible,
                Date = ditem.fum,
                Tipo = ditem.tipodistrib,
                LimiteVale = ditem.limitevale,
                LimiteCredito = ditem.limitecredito                
            };
            var q = cctx.Pagos.Where(i => i.distrib == ditem.distrib && i.status == "AP");
            var primero = q.OrderBy(i => i.fum)
                .FirstOrDefault();
            if (primero != null)
                model.PrimerPago = primero.fum;

            var ultimos = q.OrderByDescending(i => i.fum)
                .Select(i => new Models.Pago {
                    Fecha = i.fecha.Value, 
                    Descuento = i.descuento.Value,
                    Subtotal = i.subtotal.Value, 
                    Importe = i.importe.Value
                })
               .Take(10);
            ViewBag.ultimos = ultimos;

            var qd = cctx.PlanPagos.Where(i =>
                i.distrib == ditem.distrib
                && i.saldo > 0
                && i.status == "AP");
            var saldo = qd.Any() ?
                qd.Sum(i => i.saldo) : 0;
            ViewBag.saldodis = saldo;

            var cliente = cctx.Clientes.Where(i => i.idcliente == item.ValeCliente.idcliente).Single();
            var suc = ctxc.Sucursales.Where(i => i.idsucursal == cliente.idsucursal).Single();

            var qp = cctx.PlanPagos.Where(i => 
                i.cliente == cliente.cliente && i.succliente == suc.sucursal
                && i.saldo > 0
                && i.status == "AP");
            var saldodis = qp.Where(i => i.distrib == ditem.distrib).Any() ?
                qp.Where(i => i.distrib == ditem.distrib).Sum(i => i.saldo) : 0;
            ViewBag.saldocli = saldodis;

            var saldoot = qp.Where(i => i.distrib != ditem.distrib).Any() ?
                qp.Where(i => i.distrib != ditem.distrib).Sum(i => i.saldo) : 0;            
            ViewBag.saldoOtros = saldoot;            

            var credito = new Models.SolicitudCliente
            {
                Vale = item.vale, 
                ApMaterno = cliente.apmaterno, 
                ApPaterno = cliente.appaterno, 
                Electronica = item.electronica, 
                Id = cliente.idcliente, 
                Cliente = cliente.cliente, 
                Nombre = cliente.nombre, 
                MontoVale = item.ValeCliente.cantidad,
                Monto = item.monto, 
                Faltante = item.monto - item.ValeCliente.cantidad
            };
            ViewBag.credito = credito;

            var uid = this.User.GetUID();
            if(item.idusuarioAprobacion.HasValue)
                ViewBag.mia = uid == item.idusuarioAprobacion;
            ViewBag.completado = item.fechaAprobacion.HasValue;

            ViewBag.id = id;
            return View(model);
        }
        [HttpPost]
        public ActionResult Take(Guid id)
        {
            var proxy = CommonServiceLocator.ServiceLocator.Current.GetInstance<Common.ServiceContracts.ICreditoValeService>();
            var uid = this.User.GetUID();
            if (!uid.HasValue)
                throw new NotSupportedException();
            proxy.Update(id, uid.Value);
            return Json(new { success = true, txt = "hello world" });
        }

        [HttpPost]
        public ActionResult Reply(Guid id, decimal? monto, decimal? credito, bool? electronica, string res)
        {
            var proxy = CommonServiceLocator.ServiceLocator.Current.GetInstance<Common.ServiceContracts.ICreditoValeService>();
            var request = new ReplyCreditoRequest
            {
                Id = id
            };
            if (res == "aceptar")
            {
                request.Approved = true;
                request.LimiteCredito = credito;
                request.MontoVale = monto;
                request.Electronica = electronica;
            }
            if (res == "rechazar")
            {
                request.Approved = false;
            }
            
            proxy.Complete(request);
            return Json(new { success = true, txt = "replied: " + res });
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using SirCoPOS.BusinessLogic;
using Microsoft.SqlServer.Server;

namespace SirCoPOS.WebServices.Activities
{

    public sealed class RequestActivity : CodeActivity
    {
        public InArgument<Common.Entities.SolicitudCreditoRequest> Request { get; set; }
        public OutArgument<Common.Entities.SolicitudCreditoResponse> Response { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            var now = DateTime.Now;
            var ctx = new DataAccess.SirCoPOSDataContext();

            var request = context.GetValue(this.Request);

            var item = new DataAccess.SirCoPOS.SolicitudCreditoVale
            {
                Id = Guid.NewGuid(),
                date = DateTime.Now, 
                electronica = request.Electronica, 
                idusuario = request.idusuario, 
                monto = request.Monto, 
                vale = request.Vale
            };
            ctx.SolicitudesCreditoVales.Add(item);
            ctx.SaveChanges();

            //-----------------------------
            bool? processing = null;
            var cctx = new DataAccess.SirCoCreditoDataContext();
            var ctxc = new DataAccess.SirCoControlDataContext();

            var vale = ctx.ValesCliente.Where(i => i.vale == request.Vale).Single();

            var ditem = cctx.Distribuidores.Where(i => i.iddistrib == item.ValeCliente.iddistrib).Single();
            if (ditem.tipodistrib == "NORMAL" && request.Monto == vale.cantidad)
            {
                if (request.Electronica && ditem.solocalzado == 1)
                {
                    ditem.solocalzado = 0;
                    cctx.SaveChanges();
                    processing = false;
                }
                //if (request.Electronica && ditem.solocalzado == 1)
                //{
                //    var q = cctx.Pagos.Where(i => i.distrib == ditem.distrib && i.status == "AP");
                //    var primero = q.OrderBy(i => i.fum)
                //        .FirstOrDefault();
                //    var fchecar = primero.fecha.Value.AddMonths(6);
                //    if (now > fchecar)
                //    {
                //        var dif = ditem.limitecredito - ditem.disponible;
                //        if (dif > 0)
                //        {
                //            var por = dif / ditem.limitecredito;
                //            if (por <= .1m)
                //            {
                //                ditem.solocalzado = 0;
                //                cctx.SaveChanges();
                //                processing = false;
                //            }
                //        }
                //        else
                //        {
                //            ditem.solocalzado = 0;
                //            cctx.SaveChanges();
                //            processing = false;
                //        }
                //    }
                //}
            }
            //-----------------------------

            var response = new Common.Entities.SolicitudCreditoResponse
            {
                Id = item.Id, 
                Processing = processing
            };
            context.SetValue(this.Response, response);
        }
    }
}

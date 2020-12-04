using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;

namespace SirCoPOS.WebServices.Activities
{

    public sealed class CompleteActivity : CodeActivity
    {
        public InArgument<Common.Entities.ReplyCreditoRequest> Reply { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            var ctx = new DataAccess.SirCoPOSDataContext();
            var ctxc = new DataAccess.SirCoCreditoDataContext();
            var reply = context.GetValue(this.Reply);

            var item = ctx.SolicitudesCreditoVales.Where(i => i.Id == reply.Id).Single();
            item.fechaAprobacion = DateTime.Now;
            item.Approved = reply.Approved;
            if (reply.Approved)
            {
                item.electronicaAprobacion = reply.Electronica;
                item.montoAprobacion = reply.MontoVale - item.ValeCliente.cantidad;
                item.ValeCliente.cantidad = reply.MontoVale.Value;

                var dis = ctxc.Distribuidores.Where(i => i.iddistrib == item.ValeCliente.iddistrib).Single();
                if (reply.LimiteCredito.HasValue && reply.LimiteCredito.Value != dis.limitecredito)
                {
                    item.creditoAprobacion = reply.LimiteCredito - dis.limitecredito;
                    dis.limitecredito = reply.LimiteCredito;
                    if (reply.Electronica.HasValue)
                        dis.solocalzado = (short)(reply.Electronica.Value ? 0 : 1);
                    dis.fum = DateTime.Now;
                    ctxc.SaveChanges();
                }
            }
            ctx.SaveChanges();
        }
    }
}

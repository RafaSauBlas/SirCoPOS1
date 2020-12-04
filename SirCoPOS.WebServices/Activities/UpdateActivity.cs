using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;

namespace SirCoPOS.WebServices.Activities
{

    public sealed class UpdateActivity : CodeActivity
    {
        public InArgument<Guid> GID { get; set; }
        public InArgument<int> UID { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            var ctx = new DataAccess.SirCoPOSDataContext();

            var gid = context.GetValue(this.GID);
            var uid = context.GetValue(this.UID);

            var item = ctx.SolicitudesCreditoVales.Where(i => i.Id == gid).Single();
            if (item.fechaAprobacion.HasValue)
                throw new NotSupportedException();

            item.idusuarioAprobacion = uid;
            item.fechaRevision = DateTime.Now;
            ctx.SaveChanges();
        }
    }
}

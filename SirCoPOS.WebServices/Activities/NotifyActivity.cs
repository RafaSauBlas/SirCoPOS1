using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;

namespace SirCoPOS.WebServices.Activities
{

    public sealed class NotifyActivity : CodeActivity
    {
        public InArgument<Guid> GID { get; set; }
        public InArgument<string> Client { get; set; }
        public InArgument<bool> Complete { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            var gid = context.GetValue(this.GID);
            var complete = context.GetValue(this.Complete);
            var client = context.GetValue(this.Client);

            var proxy = CommonServiceLocator.ServiceLocator.Current.GetInstance<Common.ServiceContracts.IServiceDuplexReply>();
            proxy.Update(client, gid, complete);
        }
    }
}

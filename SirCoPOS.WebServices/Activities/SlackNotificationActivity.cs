using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using System.Net;
using Newtonsoft.Json.Linq;

namespace SirCoPOS.WebServices.Activities
{

    public sealed class SlackNotificationActivity : CodeActivity
    {
        public InArgument<Guid> GID { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            var gid = context.GetValue(this.GID);

            var ctx = new DataAccess.SirCoPOSDataContext();
            var item = ctx.SolicitudesCreditoVales.Where(i => i.Id == gid).Single();

            var accessToken = "xoxp-191473233024-192189952148-257230418000-7fcd073114b4ec5c16bce9ff43a8614b";
            accessToken = "xoxb-191473233024-810241754325-eTaqvz33dXjaOnEqnywvAy8a";
            var url = "https://slack.com/api/chat.postMessage";
            var cli = new WebClient();
            cli.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
            cli.Headers[HttpRequestHeader.Authorization] = $"Bearer {accessToken}";

            dynamic data = new JObject();
            data.channel = "U5N5KU04C";
            data.text = $"vale: {item.vale}\nSolicita: {item.monto} <http://localhost:39075/Solicitudes/Credito/{gid}|detalle>";
            data.as_user = true;

            var response = cli.UploadString(url, data.ToString());
        }
    }
}

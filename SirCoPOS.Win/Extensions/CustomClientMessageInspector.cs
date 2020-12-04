using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Win.Extensions
{
    public class CustomClientMessageInspector : IClientMessageInspector
    {
        public void AfterReceiveReply(ref Message reply, object correlationState)
        {
            
        }

        public object BeforeSendRequest(ref Message request, IClientChannel channel)
        {
            //var header = MessageHeader.CreateHeader("MachineName", "s", Environment.MachineName);
            HttpRequestMessageProperty requestMessage;
            if (request.Properties.ContainsKey(HttpRequestMessageProperty.Name))
                requestMessage = (HttpRequestMessageProperty)request.Properties[HttpRequestMessageProperty.Name];
            else
            {
                requestMessage = new HttpRequestMessageProperty();
                request.Properties[HttpRequestMessageProperty.Name] = requestMessage;
            }
            requestMessage.Headers.Add("MachineName", Environment.MachineName);
            return null;
        }
    }
}

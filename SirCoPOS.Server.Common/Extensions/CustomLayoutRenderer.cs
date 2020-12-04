using NLog;
using NLog.Config;
using NLog.LayoutRenderers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace SirCoPOS.Server.Common.Extensions
{
    [LayoutRenderer("custom")]
    public class CustomLayoutRenderer : LayoutRenderer
    {
        [RequiredParameter]
        public string Mode { get; set; }
        protected override void Append(StringBuilder builder, LogEventInfo logEvent)
        {
            var xml = logEvent.FormattedMessage;
            bool? isRequest = null;
            var xdoc = XDocument.Parse(xml);
            var source = xdoc.Root.Attribute("Source").Value;
            if (source == "ServiceLevelReceiveRequest")
                isRequest = true;                
            if (source == "ServiceLevelSendReply")
                isRequest = false;

            if (!isRequest.HasValue)
                return;

            if (Mode == "isrequest")
            {
                if (isRequest.Value)
                    builder.Append("1");
                else
                    builder.Append("0");
            }
            switch (Mode)
            {
                case "action":
                    {
                        var xnm = new XmlNamespaceManager(new NameTable());
                        xnm.AddNamespace("x", "http://schemas.microsoft.com/2004/06/ServiceModel/Management/MessageTrace");
                        if (isRequest.Value)
                        {
                            var item = xdoc.XPathSelectElement("/x:MessageLogTraceRecord/x:HttpRequest/x:WebHeaders/x:SOAPAction", xnm);
                            var txt = item.Value.Trim('"');
                            builder.Append(txt);
                        }
                        else
                        {
                            xnm.AddNamespace("s", "http://schemas.xmlsoap.org/soap/envelope/");
                            xnm.AddNamespace("y", "http://schemas.microsoft.com/ws/2005/05/addressing/none");
                            var item = xdoc.XPathSelectElement("/x:MessageLogTraceRecord/s:Envelope/s:Header/y:Action", xnm);
                            var txt = item.Value;
                            builder.Append(txt);
                        }
                    }
                    break;
                case "userid":
                    {
                        var xnm = new XmlNamespaceManager(new NameTable());
                        xnm.AddNamespace("x", "http://schemas.microsoft.com/2004/06/ServiceModel/Management/MessageTrace");
                        if (isRequest.Value)
                        {
                            var item = xdoc.XPathSelectElement("/x:MessageLogTraceRecord/x:HttpRequest/x:WebHeaders/x:UserId", xnm);
                            if (item != null)
                            {
                                var txt = item.Value;
                                builder.Append(txt);
                            }
                        }
                    }
                    break;
                case "sucursal":
                    {
                        var xnm = new XmlNamespaceManager(new NameTable());
                        xnm.AddNamespace("x", "http://schemas.microsoft.com/2004/06/ServiceModel/Management/MessageTrace");
                        if (isRequest.Value)
                        {
                            var item = xdoc.XPathSelectElement("/x:MessageLogTraceRecord/x:HttpRequest/x:WebHeaders/x:Sucursal", xnm);
                            if (item != null)
                            {
                                var txt = item.Value;
                                builder.Append(txt);
                            }
                        }
                    }
                    break;
                case "machine":
                    {
                        var xnm = new XmlNamespaceManager(new NameTable());
                        xnm.AddNamespace("x", "http://schemas.microsoft.com/2004/06/ServiceModel/Management/MessageTrace");
                        if (isRequest.Value)
                        {
                            var item = xdoc.XPathSelectElement("/x:MessageLogTraceRecord/x:HttpRequest/x:WebHeaders/x:MachineName", xnm);
                            if (item != null)
                            {
                                var txt = item.Value;
                                builder.Append(txt);
                            }
                        }
                    }
                    break;
                default:
                    break;
            }            
        }
    }
}

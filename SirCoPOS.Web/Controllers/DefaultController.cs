using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using Newtonsoft.Json.Linq;
using NLog;
using SirCoPOS.DataAccess;

namespace SirCoPOS.Web.Controllers
{
    [RoutePrefix("api")]
    public class DefaultController : ApiController
    {
        private readonly ILogger _log;
        private readonly BusinessLogic.Process _process;
        public DefaultController(ILogger log, BusinessLogic.Process process)
        {
            _process = process;
            _log = log;
        }

        [Route("test")]
        public string test()
        {
            return DateTime.Now.ToString("s");
        }
        [Route("testError")]
        public string testError()
        {
            throw new NotImplementedException();
        }
        [Route("testServiceError")]
        public string testServiceError()
        {
            var url = $"{Url.Content("~/")}/DataService.svc";
            var proxy = System.ServiceModel.ChannelFactory<Common.ServiceContracts.IDataService>
                .CreateChannel(new System.ServiceModel.BasicHttpBinding(), new System.ServiceModel.EndpointAddress(url));
            proxy.test();            
            return "OK";
        }
        [Route("debug")]
        public string debug(string gid)
        {
            var url = ConfigurationManager.AppSettings["svcUrl"] ?? Url.Content("~/");

            var ctx = new DataAccess.SirCoLogsDataContext();
            var g = Guid.Parse(gid);
            var msg = ctx.Messages.Where(i => i.GID == g && i.IsRequest).Single();

            var rem = msg.Action.Replace("http://tempuri.org/", "");
            var name = rem.Substring(0, rem.IndexOf("/"));

            var svc = System.Configuration.ConfigurationManager.AppSettings[$"svc:{name}"];
            if (!url.EndsWith("/"))
                url += "/";
            url += svc;

            var reader = XmlReader.Create(new StringReader(msg.Data));
            var xdoc = XDocument.Load(reader);
            var ns = new XmlNamespaceManager(reader.NameTable);
            ns.AddNamespace("ns", "http://schemas.microsoft.com/2004/06/ServiceModel/Management/MessageTrace");
            ns.AddNamespace("s", "http://schemas.xmlsoap.org/soap/envelope/");

            var method = xdoc.XPathSelectElement("ns:MessageLogTraceRecord/ns:HttpRequest/ns:Method", ns).Value;

            var node = xdoc.XPathSelectElement("ns:MessageLogTraceRecord/ns:HttpRequest/ns:WebHeaders", ns);
            var headers = node.Elements().ToDictionary(i => i.Name.LocalName, i => i.Value.ToString());

            var xns = XNamespace.Get("http://schemas.xmlsoap.org/soap/envelope/");
            var nenv = xdoc.Root.Element(xns + "Envelope");
            var envelope = nenv.ToString();

            var xml = new XmlDocument();
            xml.LoadXml(envelope);
            var nd = xml.SelectSingleNode("s:Envelope/s:Header", ns);
            nd.ParentNode.RemoveChild(nd);
            var xenv = xml.OuterXml;

            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Timeout = (int)TimeSpan.FromMinutes(15).TotalMilliseconds;
            request.Headers.Add("SOAPAction", headers["SOAPAction"]);

            var hnames = new string[] 
            {
                "UserId",
                "Sucursal"
            };
            foreach (var item in hnames)
            {
                request.Headers.Add(item, headers[item]);
            }
            
            request.ContentType = headers["Content-Type"];
            request.Accept = "text/xml";
            request.Method = method;
            using (Stream requestStream = request.GetRequestStream())
            {
                using (StreamWriter requestWriter = new StreamWriter(requestStream))
                {
                    requestWriter.Write(xenv);
                }
            }
            WebResponse response = request.GetResponse();
            string responseEnvelope = null;
            using (Stream responseStream = response.GetResponseStream())
            {
                using (StreamReader responseReader = new StreamReader(responseStream))
                {
                    responseEnvelope = responseReader.ReadToEnd();
                }
            }

            return responseEnvelope;
        }
        [Route("debugFault")]
        public string debugFault(string gid)
        {
            var url = ConfigurationManager.AppSettings["svcUrl"] ?? Url.Content("~/");

            var ctx = new DataAccess.SirCoLogsDataContext();
            var g = Guid.Parse(gid);
            var msg = ctx.FaultMessages.Where(i => i.Id == g).Single();

            var rem = msg.Action.Replace("http://tempuri.org/", "");
            var name = rem.Substring(0, rem.IndexOf("/"));

            var svc = System.Configuration.ConfigurationManager.AppSettings[$"svc:{name}"];
            if (!url.EndsWith("/"))
                url += "/";
            url += svc;

            var xenv = $"<s:Envelope xmlns:s=\"http://schemas.xmlsoap.org/soap/envelope/\"><s:Body>{msg.Request}</s:Body></s:Envelope>";

            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Timeout = (int)TimeSpan.FromMinutes(15).TotalMilliseconds;
            request.Headers.Add("SOAPAction", msg.Action);

            request.ContentType = "text/xml; charset=utf-8";
            request.Accept = "text/xml";
            request.Method = "POST";
            using (Stream requestStream = request.GetRequestStream())
            {
                using (StreamWriter requestWriter = new StreamWriter(requestStream))
                {
                    requestWriter.Write(xenv);
                }
            }
            WebResponse response = request.GetResponse();
            string responseEnvelope = null;
            using (Stream responseStream = response.GetResponseStream())
            {
                using (StreamReader responseReader = new StreamReader(responseStream))
                {
                    responseEnvelope = responseReader.ReadToEnd();
                }
            }

            return responseEnvelope;
        }
        [Route("callback")]
        public HttpResponseMessage Callback(string id, string msg)
        {
            var proxy = new System.ServiceModel.ChannelFactory<Common.ServiceContracts.IServiceDuplexReply>("*").CreateChannel();
            proxy.Send(id, msg);
            return Request.CreateResponse(HttpStatusCode.OK);
        }


        [Route("w_test1")]
        public string wStart()
        {
            var proxy = new System.ServiceModel.ChannelFactory<Common.ServiceContracts.ICreditoValeService>("*").CreateChannel();
            var res = proxy.Request(new Common.Entities.SolicitudCreditoRequest { 
                Vale = "010", 
                Electronica = true, 
                Monto = 16000, 
                idusuario = 0
            });
            return res.Id.ToString();
        }
        [Route("w_test2")]
        public void wStart1(string gid)
        {
            var proxy = new System.ServiceModel.ChannelFactory<Common.ServiceContracts.ICreditoValeService>("*").CreateChannel();
            proxy.Update(Guid.Parse(gid), 0);            
        }
        [Route("w_test3")]
        public void  wStart2(string gid, decimal? amount, bool? electronica)
        {
            var proxy = new System.ServiceModel.ChannelFactory<Common.ServiceContracts.ICreditoValeService>("*").CreateChannel();
            proxy.Complete(new Common.Entities.ReplyCreditoRequest { 
                Id = Guid.Parse(gid),
                MontoVale = amount, 
                Electronica = electronica
            });            
        }
        [Route("slack")]
        [HttpGet]
        public void slack()
        {
            var accessToken = "xoxp-191473233024-192189952148-257230418000-7fcd073114b4ec5c16bce9ff43a8614b";
            accessToken = "xoxb-191473233024-810241754325-eTaqvz33dXjaOnEqnywvAy8a";
            var url = "https://slack.com/api/chat.postMessage";
            var cli = new WebClient();
            cli.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
            cli.Headers[HttpRequestHeader.Authorization] = $"Bearer {accessToken}";

            dynamic data = new JObject();
            data.channel = "U5N5KU04C";
            data.text = "test7\notherl 4 4ine3 <http://apps.itnnovation.net/ProjectTracker/Pivotal/2401940/Story/169368450|this link>";
            data.as_user = true;

            var response = cli.UploadString(url, data.ToString());
        }

        [Route("procesarNotas")]
        [HttpGet]
        public void ProcesarNotas()
        {
            var ctx = new DataAccess.SirCoPOSDataContext();
            var q = ctx.Notas.Where(i => i.Venta == null)
                .OrderBy(i => i.Date);

            foreach (var item in q)
            {
                var request = BusinessLogic.Helpers.Serializer.Deserialize<Common.Entities.SaleRequest>(item.Data);
                foreach (var pitem in request.Productos)
                {
                    _process.RequestProducto(pitem.Serie, item.CajeroId);
                }
                var res = _process.Sale(request, item.CajeroId);
                item.Venta = res.Folio;
                ctx.SaveChanges();
            }            
        }
        [Route("testCancelarDevolucion")]
        public void cancelarDevolucion(string sucursal, string folio)
        {
            _process.CancelReturn(sucursal, folio, 0);
        }
        [Route("testCancelarCambio")]
        public void cancelarCambio(string sucursal, string folio)
        {
            var request = new Common.Entities.CancelSaleRequest
            {
                Sucursal = sucursal, 
                Folio = folio
            };
            _process.CancelSale(request, 0);

            var ctx = new SirCoPVDataContext();
            var pagos = ctx.PagosDetalle.Where(i => i.sucursal == sucursal && i.pago == folio
                && i.idformapago == (int)Common.Constants.FormaPago.DV);
            foreach (var item in pagos)
            {
                var suc = item.referencia.Substring(0, 2);
                var fol = item.referencia.Substring(2);

                var dev = ctx.Devoluciones.Where(i => i.sucursal == suc && i.devolvta == fol).Single();
                _process.CancelReturn(dev.sucursal, dev.devolvta, 0);
            }
        }
    }
}

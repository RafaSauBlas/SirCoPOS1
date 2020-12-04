using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Web;

namespace SirCoPOS.Server.Common.Extensions
{
    public class LogFaultDispatchMessageInspector : IDispatchMessageInspector
    {
        private static object _sync = new object();
        private Dictionary<Guid, LogMessageItem> _dic;
        public LogFaultDispatchMessageInspector()
        {
            _dic = new Dictionary<Guid, LogMessageItem>();
        }
        public object AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
        {
            if (request.IsEmpty)
                return null;

            var buffer = request.CreateBufferedCopy(int.MaxValue);
            request = buffer.CreateMessage();
            var msg = buffer.CreateMessage();
            //https://patrickdesjardins.com/blog/wcf-inspector-for-logging
            //https://docs.microsoft.com/en-us/dotnet/api/system.servicemodel.dispatcher.idispatchmessageinspector?view=netframework-4.8
            //https://github.com/NLog/NLog/wiki/How-to-write-a-custom-layout-renderer
            var gid = Guid.Parse(request.Properties["ActivityId"].ToString());
            var action = request.Headers.Action;
            var now = DateTime.Now;
            using (var xreader = msg.GetReaderAtBodyContents())
            {
                var xml = xreader.ReadOuterXml();
                _dic.Add(gid, new LogMessageItem { 
                    Action = action, 
                    StartDate = now, 
                    Request = xml 
                });
            }
            return gid;
        }

        public void BeforeSendReply(ref Message reply, object correlationState)
        {
            if (correlationState == null)
                return;

            var gid = (Guid)correlationState;
            var now = DateTime.Now;
            if (!_dic.ContainsKey(gid))
                return;
            if (reply.IsFault)
            {
                var buffer = reply.CreateBufferedCopy(int.MaxValue);
                reply = buffer.CreateMessage();
                var msg = buffer.CreateMessage();

                string xml;
                using (var xreader = reply.GetReaderAtBodyContents())
                {
                    xml = xreader.ReadOuterXml();
                }
                var data = _dic[gid];
                lock (_sync)
                {
                    var ctx = new DataAccess.SirCoLogsDataContext();
                    var item = new DataAccess.SirCoLogs.FaultMessage
                    {
                        Id = gid,
                        Action = data.Action,
                        StartDate = data.StartDate, 
                        Request = data.Request, 
                        Response = xml, 
                        EndDate = now
                    };
                    ctx.FaultMessages.Add(item);
                    ctx.SaveChanges();
                }
            }
            _dic.Remove(gid);
        }
    }
}
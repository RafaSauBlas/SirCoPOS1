using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.ServiceHost
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Single)]
    class ServiceDuplex : 
        Common.ServiceContracts.IServiceDuplex,
        Common.ServiceContracts.IServiceDuplexReply
    {
        private IDictionary<string, ICollection<Common.ServiceContracts.IServiceDuplexCallback>> _dic;
        private IDictionary<string, string> _sessions;
        public ServiceDuplex()
        {
            _dic = new Dictionary<string, ICollection<Common.ServiceContracts.IServiceDuplexCallback>>();
            _sessions = new Dictionary<string, string>();
        }
        public void Connect(string id)
        {            
            if (!_dic.ContainsKey(id))
            {
                _dic.Add(id, new HashSet<Common.ServiceContracts.IServiceDuplexCallback>());                
            }            
            OperationContext.Current.Channel.Closed += (sender, e) => {
                _dic[id].Remove((Common.ServiceContracts.IServiceDuplexCallback)sender);
                var client = (IClientChannel)sender;
                var com = (ICommunicationObject)sender;
                _sessions.Remove(client.SessionId);
                Console.WriteLine($"disconnect: {id}, session: {client.SessionId}");
            };
            var callback = OperationContext.Current.GetCallbackChannel<Common.ServiceContracts.IServiceDuplexCallback>();
            _dic[id].Add(callback);
            _sessions.Add(OperationContext.Current.SessionId, id);
            Console.WriteLine($"connect: {id}, session: {OperationContext.Current.SessionId}");
        }

        public void Ping()
        {
            var id = _sessions[OperationContext.Current.SessionId];
            Console.WriteLine($"Ping: {id}");
        }

        public void Send(string id, string msg)
        {
            Console.WriteLine("Send: {0}, msg: {1}", id, msg);
            if (_dic.ContainsKey(id) && _dic[id].Any())
            {
                Console.WriteLine("Sending: {0}, msg: {1}", id, msg);
                foreach (var item in _dic[id].ToArray())
                {
                    try
                    {
                        var client = (IClientChannel)item;
                        if (client.State == CommunicationState.Closed || client.State == CommunicationState.Faulted)
                        {
                            _dic[id].Remove(item);
                            Console.WriteLine("Remove: {0}, session: {1}", id, client.SessionId);
                        }
                        else
                        {
                            Console.WriteLine($"to session: {client.SessionId}");
                            item.Receive(msg);
                        }
                    }
                    catch
                    {
                        _dic[id].Remove(item);
                        Console.WriteLine("Error: {0}, msg: {1}", id, msg);
                    }
                }
            }
            else
                Console.WriteLine("NOT FOUND - Send: {0}, msg: {1}", id, msg);
        }

        public void Update(string id, Guid gid, bool complete)
        {
            Console.WriteLine("Update: {0}, gid: {1}, complete: {2}", id, gid, complete);
            if (_dic.ContainsKey(id) && _dic[id].Any())
            {
                Console.WriteLine("Sending: {0}, msg: {1}", id, complete);
                foreach (var item in _dic[id].ToArray())
                {
                    try
                    {
                        var client = (IClientChannel)item;
                        if (client.State == CommunicationState.Closed || client.State == CommunicationState.Faulted)
                        {
                            _dic[id].Remove(item);
                            Console.WriteLine("Remove: {0}, session: {1}", id, client.SessionId);
                        }
                        else
                        {
                            Console.WriteLine($"to session: {client.SessionId}");
                            item.Update(gid, complete);
                        }
                    }
                    catch
                    {
                        _dic[id].Remove(item);
                        Console.WriteLine("Error: {0}, msg: {1}", id, gid);
                    }
                }
            }
            else
                Console.WriteLine("NOT FOUND - Send: {0}, msg: {1}", id, gid);
        }
    }
}

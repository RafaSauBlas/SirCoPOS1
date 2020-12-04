using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.ServiceHost
{
    public class LocalService : Common.ServiceContracts.ILocalService
    {
        public LocalService()
        {
            _finger = new Helpers.FingerHelper();
        }
        private Helpers.FingerHelper _finger;
        public bool Match(byte[] current, byte[] stored)
        {
            return _finger.Verify(current, stored);
        }
        public int? Find(byte[] current, IDictionary<int, byte[]> options)
        {
            return _finger.Identify(current, options);
        }
        public bool Send(string number, string message)
        {
            throw new NotImplementedException();
        }
    }
}

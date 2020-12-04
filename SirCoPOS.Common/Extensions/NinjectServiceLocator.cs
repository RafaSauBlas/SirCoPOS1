using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Common.Extensions
{
    public class NinjectServiceLocator : CommonServiceLocator.IServiceLocator
    {
        private readonly IKernel _kernel;
        public NinjectServiceLocator(IKernel kernel)
        {
            _kernel = kernel;
        }
        public IEnumerable<object> GetAllInstances(Type serviceType)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TService> GetAllInstances<TService>()
        {
            throw new NotImplementedException();
        }

        public object GetInstance(Type serviceType)
        {
            return _kernel.Get(serviceType);
        }

        public object GetInstance(Type serviceType, string key)
        {
            return _kernel.Get(serviceType, key);
        }

        public TService GetInstance<TService>()
        {
            return _kernel.Get<TService>();
        }

        public TService GetInstance<TService>(string key)
        {
            return _kernel.Get<TService>(key);
        }

        public object GetService(Type serviceType)
        {
            throw new NotImplementedException();
        }
    }
}

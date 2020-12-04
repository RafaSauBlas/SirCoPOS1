using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Utilities.Helpers
{
    public static class Singleton<T>
        where T : class, new()
    {
        private static Lazy<T> _lazy;
        private static T _instance;

        static Singleton()
        {
            Set();
        }

        public static void Set()
        {
            _instance = default(T);
            _lazy = new Lazy<T>(() => new T());
        }

        public static void Set(Func<T> create)
        {
            _lazy = new Lazy<T>(create);
        }

        public static void Set(T instance)
        {
            _instance = instance;
        }

        public static T Instance
        {
            get
            {
                if (_instance == default(T))
                    _instance = _lazy.Value;
                return _instance;
            }
        }
    }
}

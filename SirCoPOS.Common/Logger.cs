using CommonServiceLocator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using System.Diagnostics;

namespace SirCoPOS.Common
{
    public class Logger
    {
        private static object _sync;
        static Logger()
        {
            _sync = new object();
        }
        public static void Fatal(Exception ex, string module)
        {
            lock (_sync)
            {
                var log = ServiceLocator.Current.GetInstance<ILogger>();
                var e = new NLog.LogEventInfo();
                e.Level = LogLevel.Fatal;
                e.Exception = ex;
                e.Properties["module"] = module;
                e.SetStackTrace(new StackTrace(), 1);
                log.Log(e);
            }
        }
    }
}

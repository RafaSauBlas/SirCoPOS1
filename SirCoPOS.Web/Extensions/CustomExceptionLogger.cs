using CommonServiceLocator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.ExceptionHandling;
using NLog;

namespace SirCoPOS.Web.Extensions
{
    public class CustomExceptionLogger : ExceptionLogger
    {
        public override void Log(ExceptionLoggerContext context)
        {
            var ex = context.Exception;
            if (ex == null)
                return;

            //var log = ServiceLocator.Current.GetInstance<ILogger>();
            //log.Fatal(ex, "WebApi");
            Common.Logger.Fatal(ex, "WebApi");
        }
    }
}
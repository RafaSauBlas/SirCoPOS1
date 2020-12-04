using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NLog;
using CommonServiceLocator;

namespace SirCoPOS.Web.Extensions
{
    public class ExceptionFilterAttribute : ActionFilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            var ex = filterContext.Exception;
            if (ex == null)
                return;
            //var log = ServiceLocator.Current.GetInstance<ILogger>();
            //log.Fatal(ex, "Web");
            Common.Logger.Fatal(ex, "Web");
        }
    }
}
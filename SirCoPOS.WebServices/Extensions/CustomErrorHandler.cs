using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Web;

namespace SirCoPOS.WebServices.Extensions
{
    public class CustomErrorHandler : IErrorHandler
    {
        public bool HandleError(Exception error)
        {
            return false;
        }

        public void ProvideFault(Exception error, MessageVersion version, ref Message fault)
        {
            if (error == null)
                return;

            //var log = ServiceLocator.Current.GetInstance<ILogger>();
            //log.Fatal(error, "WCF");
            Common.Logger.Fatal(error, "WCF");

            if (HttpContext.Current == null)
            {
                return;
            }

            Elmah.ErrorSignal.FromCurrentContext().Raise(error);
        }
    }
}
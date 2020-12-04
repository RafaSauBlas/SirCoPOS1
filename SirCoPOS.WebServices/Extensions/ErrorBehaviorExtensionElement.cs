using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Configuration;
using System.Web;

namespace SirCoPOS.WebServices.Extensions
{
    public class ErrorBehaviorExtensionElement : BehaviorExtensionElement
    {
        public override Type BehaviorType
        {
            get
            {
                return typeof(ServiceErrorBehaviorAttribute);
            }
        }

        protected override object CreateBehavior()
        {
            return new ServiceErrorBehaviorAttribute(typeof(CustomErrorHandler));
        }
    }
}
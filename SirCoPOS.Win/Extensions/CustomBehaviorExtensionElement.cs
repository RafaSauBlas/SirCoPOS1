using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Configuration;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Win.Extensions
{
    public class CustomBehaviorExtensionElement : BehaviorExtensionElement
    {
        public override Type BehaviorType => typeof(CustomEndpointBehavior);

        protected override object CreateBehavior()
        {
            return new CustomEndpointBehavior();
        }
    }
}

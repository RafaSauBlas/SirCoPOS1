using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Win.Helpers.Debug
{
    [DataContract(Namespace = "http://tempuri.org/")]
    public class Sale
    {
        [DataMember(Name = "item")]
        public Common.Entities.SaleRequest Item { get; set; }
    }
}

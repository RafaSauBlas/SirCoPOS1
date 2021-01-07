using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Win.Helpers.Debug
{
    [DataContract(Namespace = "http://tempuri.org/")]
    public class Return
    {
        [DataMember(Name = "item")]
        public Common.Entities.ReturnRequest Item { get; set; }
    }
}

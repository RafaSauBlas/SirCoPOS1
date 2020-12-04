using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Common.Entities
{
    public class ChangeItem
    {
        public string OldItem { get; set; }
        public string NewItem { get; set; }
        public decimal? Precio { get; set; }
        [IgnoreDataMember]
        public bool Corrida { get; set; }
        [IgnoreDataMember]
        public decimal? Pago { get; set; }
    }
}

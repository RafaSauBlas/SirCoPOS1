using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Utilities.Extensions
{
    [MetadataAttribute]
    public class MetadataFormaPagoAttribute : Attribute
    {
        public MetadataFormaPagoAttribute(Common.Constants.FormaPago pago)
        {
            this.FormaPago = pago;
        }
        public Common.Constants.FormaPago FormaPago { get; private set; }
    }
}

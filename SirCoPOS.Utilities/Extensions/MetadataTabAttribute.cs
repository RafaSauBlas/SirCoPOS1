using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Utilities.Extensions
{
    [MetadataAttribute]
    public class MetadataTabAttribute : Attribute
    {
        public MetadataTabAttribute(Utilities.Constants.TabType tab)            
        {
            this.Tab = tab;
        }
        public Utilities.Constants.TabType Tab { get; private set; }
    }
}

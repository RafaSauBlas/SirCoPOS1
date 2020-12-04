using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Utilities.Extensions
{
    [MetadataAttribute]
    public class MetadataModalAttribute : Attribute
    {
        public MetadataModalAttribute(Constants.Modals modal)
        {
            this.Modal = modal;
        }
        public Constants.Modals Modal { get; private set; }
    }
}

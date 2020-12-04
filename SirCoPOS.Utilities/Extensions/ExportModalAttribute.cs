using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SirCoPOS.Utilities.Extensions
{
    public class ExportModalAttribute : ExportAttribute
    {
        public ExportModalAttribute()
            : base("modal", typeof(UserControl))
        { 
        
        }
    }
}

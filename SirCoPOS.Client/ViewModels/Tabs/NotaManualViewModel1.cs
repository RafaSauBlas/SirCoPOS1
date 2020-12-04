using SirCoPOS.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Client.ViewModels.Tabs
{
    class NotaManualViewModel1 : CajaViewModel
    {
        protected override SaleType Tipo => SaleType.Note;
        public NotaManualViewModel1()
        {
            if (this.IsInDesignMode)
            { 
            
            }
        }
    }
}

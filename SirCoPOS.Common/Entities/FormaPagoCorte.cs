using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Common.Entities
{
    public class FormaPagoCorte
    {
        public Constants.FormaPago FormaPago { get; set; }
        public int Count { get; set; }
        public decimal? Total { get; set; }
    }
}

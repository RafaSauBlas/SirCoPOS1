using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Common.Entities
{
    public class RegisterValeRequest
    {
        public string Vale { get; set; }
        public Cliente Cliente { get; set; }
        public decimal? Cantidad { get; set; }
        public bool Electronica { get; set; }
    }
}

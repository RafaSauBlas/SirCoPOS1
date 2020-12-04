using AutoMapper.Configuration.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Common.Entities
{
    public class ItemCorte
    {
        public Constants.FormaPago FormaPago { get; set; }
        public int Entregar { get; set; }
        public decimal Amount { get; set; }
    }
}

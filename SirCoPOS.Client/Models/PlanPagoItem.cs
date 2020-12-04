using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Client.Models
{
    public class PlanPagoItem
    {
        public int Number { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
    }
}

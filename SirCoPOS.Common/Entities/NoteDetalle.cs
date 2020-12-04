using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Common.Entities
{
    public class NoteDetalle
    {
        public string Serie { get; set; }
        public decimal AmountOriginal { get; set; }
        public decimal Amount { get; set; }
        public string Comments { get; set; }
    }
}

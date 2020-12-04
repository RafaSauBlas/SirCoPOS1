using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Common.Entities
{
    public class ReplyCreditoRequest
    {
        public Guid Id { get; set; }
        public bool Approved { get; set; }
        public decimal? LimiteCredito { get; set; }
        public decimal? MontoVale { get; set; }
        public bool? Electronica { get; set; }
    }
}

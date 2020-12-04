using SirCoPOS.Common.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Common.Entities
{
    public class ScanDevolucionResponse
    {
        public bool Success { get; set; }
        public Status? Status { get; set; }
        public ProductoDevolucion Producto { get; set; }
    }
}

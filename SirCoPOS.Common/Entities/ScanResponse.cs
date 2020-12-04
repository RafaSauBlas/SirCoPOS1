using SirCoPOS.Common.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Common.Entities
{
    public class ScanResponse
    {
        public Status Status { get; set; }
        public int? UsuarioCajaId { get; set; }
        public Producto Producto { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Common.Entities
{
    public class NoteRequest
    {
        public int VendedorId { get; set; }
        public string Sucursal { get; set; }
        public IEnumerable<NoteDetalle> Items { get; set; }
        public IEnumerable<NotePago> Pagos { get; set; }
    }
}

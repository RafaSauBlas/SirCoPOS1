using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Common.Entities
{
    public class NoteHeader
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Sucursal { get; set; }
        public int CajeroId { get; set; }
        public decimal Total { get; set; }
    }
}

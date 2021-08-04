using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Common.Entities
{
    public class Devolucion
    {
        public string Sucursal { get; set; }
        public string Folio { get; set; }
        public decimal Disponible { get; set; }
        public int? ClientId { get; set; }
        public string Estatus { get; set; }
    }
}

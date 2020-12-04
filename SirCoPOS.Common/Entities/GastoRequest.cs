using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Common.Entities
{
    public class GastoRequest
    {
        public int Tipo { get; set; }
        public decimal Monto { get; set; }
        public string Sucursal { get; set; }
        public int CajeroId { get; set; }
        public int SolicitaId { get; set; }
        public string Descripcion { get; set; }
    }
}

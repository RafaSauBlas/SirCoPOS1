using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Client.Messages
{
    class NotaItem : Utilities.Messages.ModalResponse
    {
        public decimal? Precio { get; set; }
        public string Razon { get; set; }
        public int? TipoRazon { get; set; }
    }
}

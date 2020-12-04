using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Client.Messages
{
    public class DescuentoEspecial : Utilities.Messages.ModalResponse
    {
        public Common.Entities.DescuentoAdicional Descuento { get; set; }
        public string Descripcion { get; set; }
    }
}

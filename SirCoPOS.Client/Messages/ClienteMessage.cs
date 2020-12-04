using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Client.Messages
{
    class ClienteMessage : Utilities.Messages.ModalResponse
    {
        public Common.Entities.Cliente Cliente { get; set; }
    }
    class NuevoClienteMessage : Utilities.Messages.ModalResponse
    {
        public Models.NuevoCliente Cliente { get; set; }
    }
}

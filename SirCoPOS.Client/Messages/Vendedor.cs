using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Client.Messages
{
    public class Vendedor : Utilities.Messages.ModalResponse
    {
        public Common.Entities.Empleado Empleado { get; set; }
    }
}

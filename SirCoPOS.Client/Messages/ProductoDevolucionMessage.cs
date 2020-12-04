using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Client.Messages
{
    class ProductoDevolucionMessage : Utilities.Messages.ModalResponse
    {
        public Models.ProductoDevolucion Item { get; set; }
    }
}

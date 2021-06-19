using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Utilities.Messages
{
    public class OpenModal
    {
        public Utilities.Constants.Modals Name { get; set; }
        public int opcion { get; set; }
        public Guid GID { get; set; }
        public bool Close { get; set; }
    }

    public class OpenModalItem : OpenModal
    { 
        public Utilities.Interfaces.IProducto Item { get; set; }
    }
    public class OpenModalDevolucionItem : OpenModal
    {
        public OpenModalDevolucionItem()
        {
            this.Name = Utilities.Constants.Modals.devolucion; 
        }
        public Common.Entities.ProductoDevolucion Item { get; set; }
    }
}

using SirCoPOS.Common.Constants;
using SirCoPOS.Utilities.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Utilities.Messages
{
    public class OpenPago
    {
        public FormaPago FormaPago { get; set; }
        public Guid GID { get; set; }
        //public decimal Total { get; set; }
        //public decimal TotalCalzado { get; set; }
        //public decimal TotalElectronica { get; set; }
        public Interfaces.ICajaBase Caja { get; set; }
        public int? ClientId { get; set; }
        public bool HasPromocionPlazo { get; set; }
        public IEnumerable<Utilities.Interfaces.IProducto> ProductosPlazos { get; set; }
    }
}

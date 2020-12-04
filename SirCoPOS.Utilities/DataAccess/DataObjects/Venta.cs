using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Utilities.DataAccess.DataObjects
{    
    public class Venta
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime Fecha { get; set; }
        public int CajeroId { get; set; }
        public int? VendedorId { get; set; }
        public int? ClienteId { get; set; }
        public string NuevoCliente { get; set; }
        public virtual ICollection<VentaArticulo> Articulos { get; set; }
        public virtual ICollection<VentaCupon> Cupones { get; set; }
        public virtual ICollection<VentaPago> Pagos { get; set; }
    }
}

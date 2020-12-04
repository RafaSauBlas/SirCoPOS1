using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Utilities.DataAccess.DataObjects
{
    public class VentaCupon
    {
        [Key]
        [Column(Order = 0)]
        public Guid VentaId { get; set; }
        [Key]
        [Column(Order = 1)]
        public int PromocionId { get; set; }
        [Key]
        [Column(Order = 2)]
        public string Cupon { get; set; }
        public virtual Venta Venta { get; set; }
    }
}

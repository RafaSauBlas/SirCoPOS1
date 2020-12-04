using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Utilities.DataAccess.DataObjects
{
    public class VentaArticulo
    {
        [Key]
        [Column(Order = 0)]
        public Guid VentaId { get; set; }
        [Key]
        [Column(Order = 1)]
        public string Serie { get; set; }
        public virtual Venta Venta { get; set; }
    }
}

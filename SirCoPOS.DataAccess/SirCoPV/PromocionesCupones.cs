using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.DataAccess.SirCoPV
{
    [Table("promocionescupones", Schema = "dbo")]
    public class PromocionesCupones
    {
        [Key]
        [Column(Order = 0)]
        public int idpromocion { get; set; }
        [Key]
        [Column(Order = 1)]
        public int idcupon { get; set; }
        [ForeignKey("idcupon")]
        public virtual Cupones Cupon { get; set; }
        [ForeignKey("idpromocion")]
        public virtual Promociones Promocion { get; set; }
    }
}

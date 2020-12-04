using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.DataAccess.SirCoPV
{
    [Table("promocionesrecurrencia", Schema = "dbo")]
    public class PromocionesRecurrencia
    {
        [Key]
        [Column(Order = 0)]
        public int idpromocion { get; set; }
        [Key]
        [Column(Order = 1)]
        public string dia { get; set; }
        [Key]
        [Column(Order = 2)]
        public string horainicio { get; set; }
	    public string horafin { get; set; }
        [ForeignKey("idpromocion")]
        public virtual Promociones Promocion { get; set; }
    }
}

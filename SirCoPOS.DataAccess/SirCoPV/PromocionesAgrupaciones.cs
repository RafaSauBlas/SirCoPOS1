using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.DataAccess.SirCoPV
{
    [Table("promocionesagrupaciones", Schema = "dbo")]
    public class PromocionesAgrupaciones
    {
        [Key]
        [Column(Order = 0)]
        public int idpromocion { get; set; }
        [Key]
        [Column(Order = 1)]
        public int idagrupacioncompra { get; set; }
        [Key]
        [Column(Order = 2)]
        public int idagrupacionpromo { get; set; }
        public int? renglon { get; set; }
        [ForeignKey("idpromocion")]
        public virtual Promociones Promocion { get; set; }
        [ForeignKey("idagrupacioncompra")]
        public virtual Agrupaciones AgrupacionCompra { get; set; }
        [ForeignKey("idagrupacionpromo")]
        public virtual Agrupaciones AgrupacionPromo { get; set; }
    }
}

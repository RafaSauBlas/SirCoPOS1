using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.DataAccess.SirCoPV
{
    [Table("pago", Schema = "dbo")]
    public class Pago
    {
        [Key]
        [Column(Order = 0)]
        public string sucursal { get; set; }
        [Key]
        [Column(Order = 1)]
        public string pago { get; set; }
        public DateTime? fecha { get; set; }
        public string estatus { get; set; }
        public int? idcajero { get; set; }
        public int? idvendedor { get; set; }
        public int? idusuario { get; set; }
        public DateTime? fum { get; set; }
        public int? idusuariocancela { get; set; }
        public DateTime? fumcancela { get; set; }
        public virtual ICollection<PagoDetalle> Detalle { get; set; }
        [Required]
        [ForeignKey("sucursal,pago")]
        public virtual Venta Venta { get; set; }
    }
}

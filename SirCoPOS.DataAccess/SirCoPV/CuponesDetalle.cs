using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.DataAccess.SirCoPV
{
    [Table("cuponesdet", Schema = "dbo")]
    public class CuponesDetalle
    {
        [Key]
        [Column(Order = 0)]
        public int idcupon { get; set; }
        [Key]
        [Column(Order = 1)]
        public string folio { get; set; }
        public string estatus { get; set; }
        public string referencia { get; set; } //TODO referencia - nuevo campo
        public int? idcliente { get; set; } //TODO idcliente - nuevo campo
        [ForeignKey("idcupon")]
        public virtual Cupones Cupon { get; set; }
    }
}

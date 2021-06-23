using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.DataAccess.SirCo
{
    [Table("dinero", Schema = "dbo")]
    public class Dinero
    {
        [Key]
        [Column(Order = 0)]
        public int idsucursal { get; set; }
        [Key]
        [Column(Order = 1)]
        public string cliente { get; set; }
        public DateTime? vigencia { get; set; }
        public decimal? importe { get; set; }
        public decimal? saldo { get; set; }
        public int? idusuario { get; set; }
        public DateTime? fum { get; set; }
        public virtual ICollection<DineroDetalle> Detalles { get; set; }
    }
}

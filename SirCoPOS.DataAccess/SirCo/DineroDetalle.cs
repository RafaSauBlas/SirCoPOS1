using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.DataAccess.SirCo
{
    [Table("dinerodet", Schema = "dbo")]
    public class DineroDetalle
    {
        [Key]
        [Column(Order = 0)]
        public int idsucursal { get; set; }
        [Key]
        [Column(Order = 1)]
        public string cliente { get; set; }
        [Key]
        [Column(Order = 2)]
        public string sucnota { get; set; }
        [Key]
        [Column(Order = 3)]
        public string nota { get; set; }
        public string descrip { get; set; }
        public DateTime? vigencia { get; set; }
        public decimal? importe { get; set; }
        public decimal? saldo { get; set; }
        public string tipo { get; set; }
        public string estatus { get; set; }
        public int? idusuario { get; set; }
        public DateTime? fum { get; set; }
        public virtual Dinero Dinero { get; set; }
    }
}

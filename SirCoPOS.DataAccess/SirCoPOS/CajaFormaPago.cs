using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.DataAccess.SirCoPOS
{
    [Table("CajaFormaPago", Schema = "dbo")]
    public class CajaFormaPago
    {
        [Key]
        [Column(Order = 0)]
        public string Sucursal { get; set; }
        [Key]
        [Column(Order = 2)]
        public byte Numero { get; set; }
        [Key]
        [Column(Order = 3)]
        public int FormaPago { get; set; }
        public int Unidades { get; set; }
        public decimal Monto { get; set; }
        [ForeignKey("Sucursal,Numero")]
        public virtual Caja Caja { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.DataAccess.SirCoPOS
{
    [Table("Reimpresiones")]
    public class Reimpresion
    {
        [Key]
        [Column(Order = 0)]
        public string Operacion { get; set; }
        [Key]
        [Column(Order = 1)]
        public string Sucursal { get; set; }
        [Key]
        [Column(Order = 2)]
        public string Folio { get; set; }
        public int Numero { get; set; }
    }
}

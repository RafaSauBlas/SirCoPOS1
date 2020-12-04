using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.DataAccess.SirCo
{
    [Table("medida", Schema = "dbo")]
    public class Medida
    {
        [Key]
        [Column("idarticulo", Order = 0)]
        public int ArticuloId { get; set; }
        [Key]
        [Column(Order = 1)]
        public string marca { get; set; }
        [Key]
        [Column(Order = 2)]
        public string estilon { get; set; }
        [Key]
        [Column(Order = 3)]
        public string medida { get; set; }
        [Key]
        [Column(Order = 4)]
        public string corrida { get; set; }
    }
}

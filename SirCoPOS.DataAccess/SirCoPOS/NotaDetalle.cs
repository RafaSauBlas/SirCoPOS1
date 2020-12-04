using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.DataAccess.SirCoPOS
{
    [Table("NotaDetalle", Schema = "dbo")]
    public class NotaDetalle
    {
        [Key]
        [Column(Order = 0)]
        public int NotaId { get; set; }
        [Key]
        [Column(Order = 1)]
        public string Serie { get; set; }
        public decimal Amount { get; set; }
        public string Coments { get; set; }
        [ForeignKey("NotaId")]
        public virtual Nota Nota { get; set; }        
    }
}

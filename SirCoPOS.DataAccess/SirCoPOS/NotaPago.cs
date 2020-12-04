using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.DataAccess.SirCoPOS
{
    [Table("NotaPago", Schema = "dbo")]
    public class NotaPago
    {
        [Key]
        public int Id { get; set; }
        public int NotaId { get; set; }
        public int FormaPagoId { get; set; }
        public decimal Amount { get; set; }
        [ForeignKey("NotaId")]
        public virtual Nota Nota { get; set; }
    }
}

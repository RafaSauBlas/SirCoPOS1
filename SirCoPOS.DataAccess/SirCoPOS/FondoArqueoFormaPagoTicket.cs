using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.DataAccess.SirCoPOS
{
    [Table("FondoArqueoFormaPagoTicket", Schema = "dbo")]
    public class FondoArqueoFormaPagoTicket
    {
        [Key]
        [Column(Order = 0)]
        public int FondoArqueoId { get; set; }
        [Key]
        [Column(Order = 1)]
        public int FormaPago { get; set; }
        public decimal Monto { get; set; }
        [ForeignKey("FondoArqueoId,FormaPago")]
        public virtual FondoArqueoFormaPago ArqueoFormaPago { get; set; }
    }
}

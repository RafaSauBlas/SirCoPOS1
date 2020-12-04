using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.DataAccess.SirCoPOS
{
    [Table("FondoArqueoFormaPago", Schema = "dbo")]
    public class FondoArqueoFormaPago
    {
        [Key]
        [Column(Order = 0)]
        public int FondoArqueoId { get; set; }
        [Key]
        [Column(Order = 1)]
        public int FormaPago { get; set; }
        public int Unidades { get; set; }
        public int ReportadoUnidades { get; set; }
        public decimal Monto { get; set; }
        public decimal ReportadoMonto { get; set; }        
        [ForeignKey("FondoArqueoId")]
        public FondoArqueo Arqueo { get; set; }
        public virtual ICollection<FondoArqueoFormaPagoTicket> Tickets { get; set; }
    }
}

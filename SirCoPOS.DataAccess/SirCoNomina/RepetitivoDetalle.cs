using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.DataAccess.SirCoNomina
{
    [Table("repetitivodet", Schema = "dbo")]
    public class RepetitivoDetalle
    {
        [Key]
        [Column(Order = 0)]
        public int idrepetitivo { get; set; }
        [Key]
        [Column(Order = 1)]
        public int idperiodo { get; set; }
        public decimal importe { get; set; }
        public decimal saldo { get; set; }
	    public string usuario { get; set; }
	    public DateTime fum { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.DataAccess.SirCoCredito
{
    [Table("calendario", Schema = "dbo")]
    public class Calendario
    {
        [Key]
        [Column(Order = 0)]
        public string tipocredito { get; set; }
        [Key]
        [Column(Order = 1)]
        public string tipo { get; set; }
        [Key]
        [Column(Order = 2)]
        public DateTime fechaini { get; set; }
        [Key]
        [Column(Order = 3)]
        public DateTime fechafin { get; set; }
	    public string folioco { get; set; }
	    public DateTime? fechaaplicarcorte { get; set; }
        public DateTime? fechaaplicar { get; set; }
        public DateTime? fechapcini { get; set; }
        public DateTime? fechapcfin { get; set; }
	    public string descrip { get; set; }
        public DateTime? fechapagocliente { get; set; }
        public DateTime? fechavencecliente { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.DataAccess.SirCoCredito
{
    [Table("cvale", Schema = "dbo")]
    public class ContraVale
    {

        [Key]
        [Column(Order = 0)]
        public string sucursal { get; set; }
        [Key]
        [Column(Order = 1)]

        public string cvale { get; set; }
      
        public string status { get; set; }
	    public DateTime? fecha { get; set; }
	    public string distrib { get; set; }
	    public string succte { get; set; }
	    public string cliente { get; set; }
	    public DateTime? caduca { get; set; }
	    public decimal? importe { get; set; }
	    public decimal? saldo { get; set; }
	    public string referenc { get; set; }
	    public string observa { get; set; }
	    public int? idusuario { get; set; }
	    public DateTime? fum { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.DataAccess.SirCoAPP
{
    [Table("valedigital", Schema = "dbo")]
    public class ValeDigital
    {
        [Key]
        [Column(Order = 0)]
        public int idvaledigital { get; set; }
        [Key]
        [Column(Order = 1)]
        public string distrib { get; set; }
        [Key]
        [Column(Order = 2)]
        public int idauxiliar { get; set; }
        [Key]
        [Column(Order = 3)]
        public int idcliente { get; set; }
        [Key]
        [Column(Order = 4)]
        public int idvale { get; set; }
        public string codigoqr { get; set; }
	    public DateTime? vigencia { get; set; }
	    public string estatus { get; set; }
	    public decimal? disponible { get; set; }
	    public bool? electronica { get; set; }
	    public bool? promocion { get; set; }
    }
}

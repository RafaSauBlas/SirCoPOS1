using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.DataAccess.SirCoCredito
{
    [Table("descuentoadicional", Schema = "dbo")]
    public class DescuentoAdicional
    {
        [Key]
        public int iddesctoadi { get; set; }
	    public DateTime? fechaini { get; set; }
	    public DateTime? fechafin { get; set; }
	    public string distribini { get; set; }
	    public string distribfin { get; set; }
	    public string sucursal { get; set; }
	    public string status { get; set; }
	    public decimal? descto { get; set; }	    
    }
}

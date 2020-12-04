using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.DataAccess.SirCoCredito
{
    [Table("promocioncred", Schema = "dbo")]
    public class PromocionCredito
    {
        [Key]
        public string idpromocioncred { get; set; }
	    public string sucursal { get; set; }
	    public string status { get; set; }
	    public DateTime? fechaaplicar { get; set; }
	    public DateTime? fechainicio { get; set; }
	    public DateTime? fechafin { get; set; }
	    public int? pagosmin { get; set; }
	    public int? pagosmax { get; set; }	
    }
}

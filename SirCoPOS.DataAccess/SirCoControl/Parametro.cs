using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.DataAccess.SirCoControl
{
    [Table("parametros", Schema = "dbo")]
    public class Parametro
    {
        [Key]
        public int idparametro { get; set; }
	    public string sucursal { get; set; }
	    public string clave { get; set; }
	    public string valor { get; set; }	
    }
}

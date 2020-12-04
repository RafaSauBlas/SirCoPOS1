using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.DataAccess.SirCoNomina
{
    [Table("periodo", Schema = "dbo")]
    public class Periodo
    {
        [Key]
        public int idperiodo { get; set; }
        public DateTime fechaini { get; set; }
	    public DateTime fechafin { get; set; }
	    public string estatus { get; set; }
    }
}

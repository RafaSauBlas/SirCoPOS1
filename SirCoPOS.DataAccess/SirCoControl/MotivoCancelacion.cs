using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.DataAccess.SirCoControl
{
    [Table("motivocancelacion", Schema = "dbo")]
    public class MotivoCancelacion
    {
        [Key]
        public int idmotivo { get; set; }
        public string tipo { get; set; }
	    public string motivo { get; set; }
    }
}

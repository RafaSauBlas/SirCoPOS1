using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.DataAccess.SirCoPV
{
    [Table("formaspago", Schema = "dbo")]
    public class FormaPago
    {
        [Key]
	    public int idformapago { get; set; }
	    public string formapago { get; set; }
	    public string descripcion { get; set; }
	    public bool pos { get; set; }
        public string promocion { get; set; }
    }
}

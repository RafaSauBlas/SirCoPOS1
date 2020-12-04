using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.DataAccess.SirCoPV
{
    [Table("descuentoespecial", Schema = "dbo")]
    public class DescuentoEspecial
    {
        [Key]
        public int iddescuentoespecial { get; set; }
	    public string razon { get; set; }
	    public byte descuento { get; set; }
        public bool devolucion { get; set; }
    }
}

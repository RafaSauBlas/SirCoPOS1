using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.DataAccess.SirCoPV
{
    [Table("seriecancelada", Schema = "dbo")]
    public class SerieCancelada
    {        
        [Key]
        [Column(Order = 0)]
        public string serie { get; set; }
        [Key]
        [Column(Order = 1)]
        public string sucursal { get; set; }  
	    public string status { get; set; }
        public int idcajerocancela { get; set; }
		public string sucventa { get; set; }  
		public string venta { get; set; }
        public int idcajeroventa { get; set; }
        public DateTime? fum { get; set; }
    }
}

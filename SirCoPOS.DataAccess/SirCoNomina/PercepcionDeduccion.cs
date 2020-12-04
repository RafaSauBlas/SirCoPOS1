using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.DataAccess.SirCoNomina
{
    [Table("percdeduc", Schema = "dbo")]
    public class PercepcionDeduccion
    {
        [Key]
        [Column(Order = 0)]
        public int idpercdeduc { get; set; }
        //[Key]
        //[Column(Order = 1)]
        public string clave { get; set; }
        //[Key]
        //[Column(Order = 2)]
        public string tiponom { get; set; }
        public string naturaleza { get; set; }
        public string descripc { get; set; } 
        public string descripl { get; set; }
    }
}

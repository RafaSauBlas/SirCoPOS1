using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.DataAccess.SirCoPV
{
    [Table("agrupaciones", Schema = "dbo")]
    public class Agrupaciones
    {
        [Key]
        public int idagrupacion { get; set; }
        public string nombre { get; set; }
        public virtual ICollection<AgrupacionesDetalle> Detalle { get; set; }
    }
}

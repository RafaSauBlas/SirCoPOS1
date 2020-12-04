using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.DataAccess.SirCoPV
{
    [Table("devolucionrazon", Schema = "dbo")]
    public class DevolucionRazon
    {
        [Key]
        public int iddevolucionrazon { get; set; }
        public string descripcion { get; set; }
        public bool comentarios { get; set; }
    }
}

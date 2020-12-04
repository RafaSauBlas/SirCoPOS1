using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.DataAccess.SirCoPOS
{
    [Table("ClienteAcceso", Schema = "dbo")]
    public class ClienteAcceso
    {
        [Key]
        public int ClienteId { get; set; }
        public Guid Codigo { get; set; }
        public DateTime FechaExpiracion { get; set; }
    }
}

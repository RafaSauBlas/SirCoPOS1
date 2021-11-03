using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.DataAccess.SirCoPOS
{
    [Table("UsuarioAcceso", Schema = "dbo")]
    public class UsuarioAcceso
    {
        [Key]
        [Column(Order =1)]
        public int idempleado { get; set; }
        [Key]
        [Column(Order = 2)]
        public DateTime entrada { get; set; }
        public string nombre { get; set; }
        public int iddepto { get; set; }
        public int idpuesto { get; set; }
        public string sucursal{ get; set; }
        public string nombrepc { get; set; }
        public DateTime salida { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.DataAccess.SirCoNomina
{
    [Table("empleado", Schema = "dbo")]
    public class Empleado
    {
        [Key]
        [Column(Order = 0)]
        public int idempleado { get; set; }
        //[Key]
        [Column(Order = 1)]
        public string clave { get; set; }
        //[Key]
        [Column(Order = 2)]
        public int iddepto { get; set; }
        //[Key]
        [Column(Order = 3)]
        public int idpuesto { get; set; }
        //[Key]
        [Column(Order = 4)]
        public string vendedor { get; set; }

        public string estatus { get; set; }
        public string usuariosistema { get; set; }
        public string password { get; set; }
        public string appaterno { get; set; }
        public string apmaterno { get; set; }
        public string nombre { get; set; }
        public int? idcliente { get; set; } //TODO idcliente - nuevo campo
        public string authkey { get; set; } //TODO authkey - nuevo campo
        public virtual ICollection<Huellas> Huellas { get; set; }
        public virtual ICollection<HuellasPOS> HuellasPOS { get; set; }
    }
}

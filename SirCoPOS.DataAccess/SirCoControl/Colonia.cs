using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.DataAccess.SirCoControl
{
    [Table("colonia", Schema = "dbo")]
    public class Colonia
    {
        [Key]
        [Column(Order = 0)]
        public int idcolonia { get; set; }
        [Key]
        [Column(Order = 2)]
        public int idestado { get; set; }
        [Key]
        [Column(Order = 1)]
        public int idciudad { get; set; }
        [Key]
        [Column(Order = 3)]
        public string colonia { get; set; }
        [Key]
        [Column(Order = 4)]
        public string codigopostal { get; set; }
        [ForeignKey("idciudad,idestado")]
        public virtual Ciudad Ciudad { get; set; }
    }
}

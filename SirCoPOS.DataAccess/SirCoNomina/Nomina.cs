using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.DataAccess.SirCoNomina
{
    [Table("nomina", Schema = "dbo")]
    public class Nomina
    {
        [Key]
        [Column(Order = 0)]
        public int idperiodo { get; set; }
        [Key]
        [Column(Order = 1)]
        public string tiponom { get; set; }
        //[Key]
        //[Column(Order = 2)]
        public string sucursal { get; set; }
        [Key]
        [Column(Order = 3)]
        public int idempleado { get; set; }
        [ForeignKey("idperiodo")]
        public virtual Periodo Periodo { get; set; }
        public virtual ICollection<NominaDetalle> Detalle { get; set; }
    }
}

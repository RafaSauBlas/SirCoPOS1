using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.DataAccess.SirCoNomina
{
    [Table("nominadet", Schema = "dbo")]
    public class NominaDetalle
    {
        [Key]
        [Column(Order = 0)]
        public int idperiodo { get; set; }
        [Key]
        [Column(Order = 1)]
        public string tiponom { get; set; }
        [Key]
        [Column(Order = 2)]
        public int idempleado { get; set; }
        [Key]
        [Column(Order = 3)]
        public int idpercdeduc { get; set; }
        [Key]
        [Column(Order = 4)]
        public int idrepetitivo { get; set; }
	    public decimal unidades { get; set; }
        public decimal? impexento { get; set; }	
        public Guid? movimiento { get; set; } //TODO nuevo - movimiento
        [ForeignKey("idperiodo,tiponom,idempleado")]
        public virtual Nomina Nomina { get; set; }
        [ForeignKey("idpercdeduc")]
        public virtual PercepcionDeduccion Tipo { get; set; }
    }
}

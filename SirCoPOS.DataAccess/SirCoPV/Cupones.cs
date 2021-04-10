using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.DataAccess.SirCoPV
{
    [Table("cupones", Schema = "dbo")]
    public class Cupones
    {
        [Key]
        public int idcupon { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public string restricciones { get; set; }
        public string estatus { get; set; }
        public string tipo { get; set; }
        public byte[] imagen { get; set; }
        public DateTime? fecha { get; set; }
        public DateTime? fecini { get; set; }
        public DateTime? fecfin { get; set; }

        public virtual ICollection<CuponesDetalle> CuponesDetalle { get; set; }
        public virtual ICollection<PromocionesCupones> PromocionCupon { get; set; }
    }
}

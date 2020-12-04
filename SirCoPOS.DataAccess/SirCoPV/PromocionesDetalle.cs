using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.DataAccess.SirCoPV
{
    [Table("promocionesdet", Schema = "dbo")]
    public class PromocionesDetalle
    {
        [Key]
        [Column(Order = 0)]
        public int idpromocion { get; set; }
        [Key]
        [Column(Order = 1)]
        public string formapago { get; set; }
        [Key]
        [Column(Order = 2)]
        public int numunidad { get; set; }
        public string tipo { get; set; }
        public decimal? descdirecto { get; set; }
        public decimal? impfijo { get; set; }
        public decimal? porcdinelec { get; set; }
        public decimal? impbono { get; set; }
        public decimal? descfijo { get; set; } //TODO descfijo - nuevo campo
        [ForeignKey("idpromocion")]
        public virtual Promociones Promocion { get; set; }
    }
}

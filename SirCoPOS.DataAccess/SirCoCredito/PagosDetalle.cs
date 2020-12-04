using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.DataAccess.SirCoCredito
{
    [Table("pagosdet", Schema = "dbo")]
    public class PagosDetalle
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 0)]
        public int idpagosdet { get; set; }
        [Key]
        [Column(Order = 1)]
        public int idpagos { get; set; }
        [Key]
        [Column(Order = 2)]
        public string sucursal { get; set; }
        [Key]
        [Column(Order = 3)]
        public string sucnot { get; set; }
        [Key]
        [Column(Order = 4)]
        public string nota { get; set; }
        [Key]
        [Column(Order = 5)]
        public int pago { get; set; }
	    public decimal? subtotal { get; set; }
	    public decimal? descuento { get; set; }
	    public decimal? descuentoadicional { get; set; }
	    public decimal? interes { get; set; }
	    public decimal? interesmoratorio { get; set; }
	    public decimal? gastoscobranza { get; set; }
	    public decimal? importe { get; set; }
	    public decimal? vencido { get; set; }
	    public decimal? descuentovencido { get; set; }
	    public int? numpago { get; set; }
	    public int? pagado { get; set; }
	    public decimal? porcdescto { get; set; }
	    public decimal? porcdesctoadicional { get; set; }
	    public decimal? porcdesctovencido { get; set; }
	    public int? idusuario { get; set; }
	    public DateTime? fum { get; set; }
        [ForeignKey("idpagos")]
        public virtual Pagos Pagos { get; set; }
    }
}

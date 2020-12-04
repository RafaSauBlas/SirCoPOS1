using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.DataAccess.SirCoPV
{
    [Table("pagodet", Schema = "dbo")]
    public class PagoDetalle
    {
        [Key]
        [Column(Order = 0)]
        public string sucursal { get; set; }
        [Key]
        [Column(Order = 1)]
        public string pago { get; set; }
        [Key]
        [Column(Order = 2)]
        public int idformapago { get; set; }
	    public int idvaledigital { get; set; }
	    public decimal? importe { get; set; }
	    public decimal? comision { get; set; }
        [Key]
        [Column(Order = 3)]
        public string observaciones { get; set; }
	    public decimal? iva { get; set; }
	    public int? idusuario { get; set; }
	    public DateTime? fum { get; set; }
        public string referencia { get; set; } //TODO referencia - nuevo campo
        public string terminacion { get; set; } //TODO terminacion - nuevo campo
        public string transaccion { get; set; }//TODO transaccion - nuevo campo
        public string vale { get; set; }//TODO vale - nuevo campo
        public string cvale { get; set; }//TODO cvale - nuevo campo
        public Guid? movimiento { get; set; } //TODO movimiento - nuevo campo
        public Guid? movimientocancela { get; set; } //TODO movimientocancela - nuevo campo

        [ForeignKey("sucursal,pago")]
        public virtual Pago Header { get; set; }
    }
}

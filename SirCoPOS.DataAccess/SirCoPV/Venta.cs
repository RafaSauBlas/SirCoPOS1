using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.DataAccess.SirCoPV
{
    [Table("venta", Schema = "dbo")]
    public class Venta
    {
        [Key]
        [Column(Order = 0)]
        public string sucursal { get; set; }
        [Key]
        [Column(Order = 1)]
        public string venta { get; set; }

        public DateTime? fecha { get; set; }
        public string estatus { get; set; }
        public int? idcajero { get; set; }
	    public int? idvendedor { get; set; }
	    public int? idusuario { get; set; }
	    public DateTime? fum { get; set; }
	    public int? idusuariocancela { get; set; }
	    public DateTime? fumcancela { get; set; }
        public string motivocancela { get; set; }

        public virtual ICollection<VentaDetalle> Detalles { get; set; }
        public virtual Pago Pago { get; set; }

        public int? idcliente { get; set; } //TODO idcliente - nuevo campo
        public bool? multiple { get; set; } //TODO multiple - nuevo campo
    }
}

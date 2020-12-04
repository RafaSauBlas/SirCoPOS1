using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.DataAccess.SirCoCredito
{
    [Table("pagos", Schema = "dbo")]
    public class Pagos
    {
        [Key]
        public int idpagos { get; set; }
        public string sucursal { get; set; }
        public string folio { get; set; }
        public string distrib { get; set; }
        public string status { get; set; }
        public DateTime? fecha { get; set; }
        public decimal? subtotal { get; set; }
        public decimal? descuento { get; set; }
        public decimal? descuentoadicional { get; set; }
        public decimal? interes { get; set; }
        public decimal? interesmoratorio { get; set; }
        public decimal? gastoscobranza { get; set; }
        public decimal? importe { get; set; }
        public decimal? vencido { get; set; }
        public decimal? descuentovencido { get; set; }
        public int? cobrador { get; set; }
        public int? idconvenio { get; set; }
        public int? idusuario { get; set; }
        public DateTime? fum { get; set; }
	    public int? idusuariocancela { get; set; }
	    public DateTime? fumcancela { get; set; }
	    public int? idusuarioautoriza { get; set; }
	    public DateTime? fumautoriza { get; set; }
        public virtual ICollection<PagosDetalle> Detalle { get; set; }      
    }
}

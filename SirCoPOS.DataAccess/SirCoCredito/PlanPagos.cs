using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.DataAccess.SirCoCredito
{
    [Table("planpagos", Schema = "dbo")]
    public class PlanPagos
    {
        [Key]
        [Column(Order = 0)]
        public string distrib { get; set; }
        [Key]
        [Column(Order = 1)]
        public string sucursal { get; set; }
        [Key]
        [Column(Order = 2)]
        public string nota { get; set; }
        [Key]
        [Column(Order = 3)]//TODO nueva llave
        public string negocio { get; set; }
        [Key]
        [Column(Order = 4)]//TODO nueva llave
        public string vale { get; set; }
        public int desctoori { get; set; }
        public string succliente { get; set; }
        public string cliente { get; set; }
        public long idcliente { get; set; }
        public DateTime? fechaaplicarcorte { get; set; }
	    public DateTime fechacompra { get; set; }
        public string status { get; set; }
        public decimal importe { get; set; }
        public decimal saldo { get; set; }
        public long pagos { get; set; }
        public string pagado { get; set; }
	    public string observacion { get; set; }
	    public long idusuario { get; set; }
	    public DateTime fum { get; set; }
        public decimal? blindaje { get; set; }
        public virtual ICollection<PlanPagosDetalle> Detalle { get; set; }
    }
}

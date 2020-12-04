using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.DataAccess.SirCoCredito
{
    [Table("planpagosdet", Schema = "dbo")]
    public class PlanPagosDetalle
    {
        [Key]
        [Column(Order = 1)]
        public string sucursal { get; set; }
        [Key]
        [Column(Order = 2)]
        public string nota { get; set; }
        [Key]
        [Column(Order = 5)]
        public DateTime fechaaplicar { get; set; }
        [Key]
        [Column(Order = 6)]
        public long pago { get; set; }

        [Key]
        [Column(Order = 3)]
        public string negocio { get; set; } //TODO nuevo campo
        [Key]
        [Column(Order = 4)]
        public string vale { get; set; } //TODO nuevo campo
        [Key]
        [Column(Order = 0)]
        public string distrib { get; set; } //TODO nuevo campo

        public long pagos { get; set; }
	    public DateTime fechavencimiento { get; set; }
	    public decimal importe { get; set; }
	    public decimal abono { get; set; }
	    public decimal descuento { get; set; }
	    public decimal interes { get; set; }
	    public decimal gastoscobranza { get; set; }
	    public string pagado { get; set; }
	    public DateTime fechapago { get; set; }
	    public char tipopago { get; set; }
	    public int cobrador { get; set; }
	    public long idpago { get; set; }
	    public long idconvenio { get; set; }
	    public long idusuario { get; set; }
	    public DateTime fum { get; set; }
        [ForeignKey("distrib,sucursal,nota,negocio,vale")]
        public virtual PlanPagos PlanPago { get; set; }
    }
}

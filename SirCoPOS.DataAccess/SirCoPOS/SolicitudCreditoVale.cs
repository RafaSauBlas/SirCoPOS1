using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.DataAccess.SirCoPOS
{
    [Table("SolicitudCreditoVale", Schema = "Credito")]
    public class SolicitudCreditoVale
    {
        public Guid Id { get; set; }
        public string vale { get; set; }
        public bool electronica { get; set; }
        public decimal monto { get; set; }
        public DateTime date { get; set; }
        public int idusuario { get; set; }

        public DateTime? fechaRevision { get; set; }
        public DateTime? fechaAprobacion { get; set; }
        public int? idusuarioAprobacion { get; set; }
        public decimal? montoAprobacion { get; set; }
        public decimal? creditoAprobacion { get; set; }
        public bool? electronicaAprobacion { get; set; }
        public bool? Approved { get; set; }

        [ForeignKey("vale")]
        public virtual ValeCliente ValeCliente { get; set; }        
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.DataAccess.SirCoPOS
{
    [Table("FondoArqueo", Schema = "dbo")]
    public class FondoArqueo
    {
        [Key]
        public int Id { get; set; }
        public int FondoId { get; set; }
        public int AuditorId { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Importe { get; set; }
        public decimal Reportado { get; set; }
        public bool Corte { get; set; }
        public virtual ICollection<FondoArqueoFormaPago> FormasPago { get; set; }
        public virtual ICollection<FondoArqueoSerie> Series { get; set; }
    }
}

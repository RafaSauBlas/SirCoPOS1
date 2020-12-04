using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.DataAccess.SirCoPOS
{
    [Table("Pago", Schema = "dbo")]
    public class Pago
    {
        [Key]
        public int Id { get; set; }
        public int ReceptorId { get; set; }
        public int EmisorId { get; set; }
        public decimal Importe { get; set; }
        public DateTime Fecha { get; set; }
        public int PeriodoId { get; set; }
    }
}

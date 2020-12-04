using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.DataAccess.SirCoPOS
{
    [Table("FondoFormaPago", Schema = "dbo")]
    public class FondoFormaPago
    {
        [Key]
        public int Id { get; set; }
        public int FondoId { get; set; }
        public bool Entrada { get; set; }
        public int FormaPago { get; set; }
        public int Cantidad { get; set; }
        public int? UsuarioId { get; set; }
        public decimal Monto { get; set; }
        public DateTime Fecha { get; set; }
        public Guid Referencia { get; set; }
        [ForeignKey("FondoId")]
        public virtual Fondo Fondo { get; set; }
    }
}

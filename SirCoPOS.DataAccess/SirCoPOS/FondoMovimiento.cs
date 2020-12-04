using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.DataAccess.SirCoPOS
{
    [Table("FondoMovimiento", Schema = "dbo")]
    public class FondoMovimiento
    {
        [Key]
        public int Id { get; set; }
        public int FondoId { get; set; }
        public decimal Importe { get; set; }
        public int? UsuarioId { get; set; }
        public bool Entrada { get; set; }
        public DateTime Fecha { get; set; }
        public Guid Referencia { get; set; }
        public string Tipo { get; set; }
        [ForeignKey("FondoId")]
        public virtual Fondo Fondo { get; set; }
    }
}

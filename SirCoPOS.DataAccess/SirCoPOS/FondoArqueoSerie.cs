using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.DataAccess.SirCoPOS
{
    [Table("FondoArqueoSerie", Schema = "dbo")]
    public class FondoArqueoSerie
    {
        [Key]
        [Column(Order = 0)]
        public int FondoArqueoId { get; set; }
        [Key]
        [Column(Order = 1)]
        public string Serie { get; set; }
        public DateTime? Reportado { get; set; }
        public int UsuarioId { get; set; }
        [ForeignKey("FondoArqueoId")]
        public virtual FondoArqueo Arqueo { get; set; }
    }
}

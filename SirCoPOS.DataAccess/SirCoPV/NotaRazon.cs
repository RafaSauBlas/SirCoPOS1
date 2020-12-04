using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.DataAccess.SirCoPV
{
    [Table("notarazon", Schema = "dbo")]
    public class NotaRazon
    {
        [Key]
        public int idnotarazon { get; set; }
        public string descripcion { get; set; }
        public bool comentarios { get; set; }
    }
}

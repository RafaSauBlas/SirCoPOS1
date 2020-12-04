using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.DataAccess.SirCoPOS
{
    [Table("Huella", Schema = "dbo")]
    public class Huella
    {
        [Key]
        public int Id { get; set; }
        public int EmpleadoId { get; set; }
        public byte[] Template { get; set; }
    }
}

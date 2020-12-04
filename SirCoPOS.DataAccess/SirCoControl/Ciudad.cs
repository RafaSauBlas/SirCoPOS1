using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.DataAccess.SirCoControl
{
    [Table("ciudad", Schema = "dbo")]
    public class Ciudad
    {
        [Key]
        [Column(Order = 0)]
        public int idciudad { get; set; }
        [Key]
        [Column(Order = 1)]
        public int idestado { get; set; }
        public string ciudad { get; set; }

        public virtual ICollection<Colonia> Colonias { get; set; }
        [ForeignKey("idestado")]
        public virtual Estado Estado { get; set; }
    }
}

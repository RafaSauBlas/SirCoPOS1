using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.DataAccess.SirCoControl
{
    [Table("estado", Schema = "dbo")]
    public class Estado
    {
        [Key]
        [Column(Order = 0)]
        public int idestado { get; set; }
        //[Key]
        //[Column(Order = 1)]
        public string estado { get; set; }
        public string abrev { get; set; }
        public virtual ICollection<Ciudad> Ciudades { get; set; }
    }
}

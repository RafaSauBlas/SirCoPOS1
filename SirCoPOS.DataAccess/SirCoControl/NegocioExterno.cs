using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.DataAccess.SirCoControl
{
    [Table("negocioexterno", Schema = "dbo")]
    public class NegocioExterno
    {
        [Key]
        [Column(Order = 0)]
        public int idnegexterno { get; set; }
        [Key]
        [Column(Order = 1)]
        public string negocio { get; set; }
        [Key]
        [Column(Order = 2)]
        public string descripcion { get; set; }
    }
}

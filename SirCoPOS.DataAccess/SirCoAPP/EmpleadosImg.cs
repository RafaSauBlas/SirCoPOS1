using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.DataAccess.SirCoAPP
{
    [Table("empleadosimg", Schema = "dbo")]
    public class EmpleadosImg
    {
        [Key]
        public int idempleado { get; set; }

        public byte[] foto { get; set; }
    }
}

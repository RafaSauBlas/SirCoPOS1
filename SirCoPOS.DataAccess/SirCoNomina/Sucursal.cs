using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.DataAccess.SirCoNomina
{
    [Table("sucursal", Schema = "dbo")]
    public class Sucursal
    {
        [Key]
        public string sucursal { get; set; }
        public string descrip { get; set; }
        public string idgrupo { get; set; }
        public int prioridad { get; set; }
    }
}

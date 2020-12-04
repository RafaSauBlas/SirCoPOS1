using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.DataAccess.SirCoCredito
{
    [Table("distribcomerciales", Schema = "dbo")]
    public class DistribuidorComercial
    {
        [Key]
        [Column(Order = 0)]
        public int iddistrib { get; set; }
        [Key]
        [Column(Order = 1)]
        public int idnegexterno { get; set; }
        [Key]
        [Column(Order = 2)]
        public string comercial { get; set; }
        [Key]
        [Column(Order = 3)]
        public string nocuenta { get; set; }
    }
}

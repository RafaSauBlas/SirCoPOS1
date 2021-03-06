using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.DataAccess.SirCoCredito
{
    [Table("valescancelados", Schema = "dbo")]
    public class ValesCancelados
    {
        [Key]
        [Column(Order = 0)]
        public int iddistrib { get; set; }
        [Key]
        [Column(Order = 1)]
        public string valera { get; set; }
        [Key]
        [Column(Order = 2)]
        public string valeini { get; set; }
        public string valefin { get; set; }
        public string distrib { get; set; }
        public int? idmotivo { get; set; }
    }
}

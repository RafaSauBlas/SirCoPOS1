using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.DataAccess.SirCoCredito
{
    [Table("distribfirmas", Schema = "dbo")]
    public class DistribuidorFirma
    {
        [Key]
        [Column(Order = 0)]
        public string distrib { get; set; }
        [Key]
        [Column(Order = 1)]
        public short principal { get; set; }
        [Key]
        [Column(Order = 2)]
        public string nombre { get; set; }
        [Key]
        [Column(Order = 3)]
        public string domicilio { get; set; }
        [Key]
        [Column(Order = 4)]
        public short numfirma { get; set; }
        public byte[] firma { get; set; }
    }
}

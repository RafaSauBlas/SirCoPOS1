using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.DataAccess.SirCoCredito
{
    [Table("valefisico", Schema = "dbo")]
    public class ValeFisico
    {
        [Key]
        [Column(Order = 0)]
        public string vale { get; set; }
        [Key]
        [Column(Order = 1)]
        public string negocio { get; set; }
        public decimal limite { get; set; }
        public decimal disponible { get; set; }
        public int iddistrib { get; set; }
        public string distrib { get; set; }
        public DateTime fecha { get; set; }
    }
}

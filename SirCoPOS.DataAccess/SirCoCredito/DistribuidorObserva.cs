using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.DataAccess.SirCoCredito
{
    [Table("distribrestricc", Schema = "dbo")]
    public class DistribuidorObserva
    {
        [Key]
        [Column(Order = 0)]
        public string distrib { get; set; }
        public string contvale { get; set; }
        public string neexvale { get; set; }
        public string observ01 { get; set; }
        public string observ02 { get; set; }
        public string observ03 { get; set; }
        public string observ04 { get; set; }
        public string observ05 { get; set; }
    }
}

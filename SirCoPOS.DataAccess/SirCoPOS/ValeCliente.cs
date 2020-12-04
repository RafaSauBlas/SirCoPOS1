using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.DataAccess.SirCoPOS
{
    [Table("ValeCliente", Schema = "Credito")]
    public class ValeCliente
    {
        [Key]
        public string vale { get; set; }
        public int iddistrib { get; set; }
	    public int idcliente { get; set; }
        public decimal cantidad { get; set; }
        public bool electronica { get; set; }
        public DateTime date { get; set; }
        public int idusuario { get; set; }
    }
}

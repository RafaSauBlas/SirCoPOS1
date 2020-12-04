using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.DataAccess.SirCoNomina
{
    [Table("repetitivo", Schema = "dbo")]
    public class Repetitivo
    {
        [Key]
		[Column(Order = 0)]
		public int idrepetitivo { get; set; }
		[Key]
		[Column(Order = 1)]
		public int idempleado { get; set; }
		[Key]
		[Column(Order = 2)]
		public int idpercdeduc { get; set; }
		public string descrip { get; set; }
		public string folio { get; set; }
		public decimal importe { get; set; }
		public decimal cuota { get; set; }
		public decimal saldo { get; set; }
		[Key]
		[Column(Order = 3)]
		public DateTime inicio { get; set; }
		public string estatus { get; set; }
		public int idcuenta { get; set; }
		public string usuario { get; set; }
		public DateTime fum { get; set; }
		public string usumodif { get; set; }
		public DateTime? fummodif { get; set; }
		public string observaciones { get; set; }
		public string hora { get; set; }
		public DateTime fin { get; set; }
    }
}

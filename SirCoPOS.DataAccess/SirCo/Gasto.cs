using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.DataAccess.SirCo
{
    [Table("gastos", Schema = "dbo")]
    public class Gasto
    {
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public int folio { get; set; }
		public decimal? cantidad { get; set; }
		public string sucursal { get; set; }
		public DateTime? fecha { get; set; }
		public short? idgasto { get; set; }
		public short? solicita { get; set; }
		public short? revisa { get; set; }
		public short? autoriza { get; set; }
		public string status { get; set; }
		public string comentarios { get; set; }
		public short? usuario { get; set; }
		public DateTime? fum { get; set; }
		public string usumodrevisa { get; set; }
		public DateTime? fummodrevisa { get; set; }
		public string usumodautoriza { get; set; }
		public DateTime? fummodautoriza { get; set; }
		public string foliosuc { get; set; }
		//[ForeignKey("idgasto")]
		//public virtual DetalleGasto DetalleGasto { get; set; }
    }
}

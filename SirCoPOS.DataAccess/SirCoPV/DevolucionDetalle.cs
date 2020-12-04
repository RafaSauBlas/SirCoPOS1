using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.DataAccess.SirCoPV
{
    [Table("devoluciondet", Schema = "dbo")]
    public class DevolucionDetalle
    {
        [Key]
        [Column(Order = 0)]
        public string sucursal { get; set; }
        [Key]
        [Column(Order = 1)]
        public string devolvta { get; set; }
        public short renglon { get; set; }          
        public string marca { get; set; }
        public string estilon { get; set; }
        public string corrida { get; set; }
        public string medida { get; set; }
        [Key]
        [Column(Order = 2)]
        public string serie { get; set; } 
        public int? idpromocion { get; set; }
	    public short? ctd { get; set; }
	    public decimal? precio { get; set; }
	    public decimal? precdesc { get; set; }
	    public decimal? costomargen { get; set; }
	    public decimal? iva { get; set; }
	    public int? idusuario { get; set; }
	    public DateTime? fum { get; set; }
        public string notas { get; set; } //TODO notas - nuevo campo
        public int? idrazon { get; set; } //TODO idrazon - nuevo campo
        public virtual Devolucion Header { get; set; }
        [ForeignKey("idrazon")]
        public DevolucionRazon Razon { get; set; }
    }
}

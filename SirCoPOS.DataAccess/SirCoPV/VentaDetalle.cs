using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.DataAccess.SirCoPV
{
    [Table("ventadet", Schema = "dbo")]
    public class VentaDetalle
    {
        [Key]
        [Column(Order = 0)]
        public string sucursal { get; set; }
        [Key]
        [Column(Order = 1)]
        public string venta { get; set; }
        [Key]
        [Column(Order = 2)]
        public string serie { get; set; }
        public short renglon { get; set; }
        public string marca { get; set; }
	    public string estilon { get; set; }
	    public string corrida { get; set; }
	    public string medida { get; set; }
        public int? idpromocion { get; set; }
        public int? idtipo { get; set; }
        public short? ctd { get; set; }
        public decimal? precio { get; set; }
	    public decimal? precdesc { get; set; }
	    public decimal? costomargen { get; set; }
	    public decimal? iva { get; set; }
	    public int? idusuario { get; set; }
	    public DateTime? fum { get; set; }
        public int? iddescuentoespecial { get; set; }//TODO iddescuentoespecial - nuevo campo
        public string descuentoespecialdesc { get; set; }//TODO descuentoespecialdesc - nuevo campo
        public byte? idpromocionnumero { get; set; }//TODO idpromocionnumero - nuevo campo
        public decimal? rebaja { get; set; } //TODO rebaja - nuevo campo
        public string notas { get; set; } //TODO notas - nuevo campo
        public int? idrazon { get; set; } //TODO idrazon - nuevo campo

        [ForeignKey("sucursal, venta")]
        public Venta Header { get; set; }
        [ForeignKey("idrazon")]
        public NotaRazon NotaRazon { get; set; }
    }
}

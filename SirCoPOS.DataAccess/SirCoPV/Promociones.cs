using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.DataAccess.SirCoPV
{
    [Table("promociones", Schema = "dbo")]
    public class Promociones
    {
        [Key]
        public int idpromocion { get; set; }
        public string nombre { get; set; }
        public string tipo { get; set; }
        public string estatus { get; set; }
        public DateTime? iniciopromo { get; set; }
        public DateTime? finpromo { get; set; }
        public byte[] imagen { get; set; }
        public int? minunicompra { get; set; }
        public decimal? minimpcompra { get; set; }
        public int? unipromo { get; set; }
        public string acumulable { get; set; }
        public string paresunicos { get; set; }
        public string clasificacion { get; set; }
        public byte? promosrequeridas { get; set; } //TODO promosrequeridas - nuevo campo
        public byte? duplicados { get; set; } //TODO duplicados - nuevo campo
        public bool? clienterequerido { get; set; } //TODO clienterequerido - nuevo campo
        public bool? empleadorequerido { get; set; } //TODO empleadorequerido - nuevo campo
        public int? empleadocantidad { get; set; } //TODO empleadocantidad - nuevo campo
        public string empleadocantidadtipo { get; set; } //TODO empleadocantidadtipo - nuevo campo
        public bool? importeticket { get; set; } //TODO importeticket - nuevo campo
        public virtual ICollection<PromocionesDetalle> Detalle { get; set; }
        public virtual ICollection<PromocionesAgrupaciones> Agrupaciones { get; set; }
        public virtual ICollection<PromocionesExclusiones> Exclusiones { get; set; }
        public virtual ICollection<PromocionesPlazas> Plazas { get; set; }
        public virtual ICollection<PromocionesRecurrencia> Recurrencia { get; set; }
        public virtual ICollection<PromocionesCupones> Cupones { get; set; }
    }
}

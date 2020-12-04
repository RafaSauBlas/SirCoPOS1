using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.DataAccess.SirCoPV
{
    [Table("devolucion", Schema = "dbo")]
    public class Devolucion
    {        
        [Key]
        [Column(Order = 0)]
        public string sucursal { get; set; }
        [Key]
        [Column(Order = 1)]
        public string devolvta { get; set; }  
        public string tipo { get; set; }   
        public DateTime? fecha { get; set; }
	    public string estatus { get; set; }
	    public string referencia { get; set; } 
        public string comentarios { get; set; }  
        public int? idcajero { get; set; }
        public int? idvendedor { get; set; }
        public int? idusuario { get; set; }
	    public DateTime? fum { get; set; }
        public int? idusuariocancela { get; set; }
	    public DateTime? fumcancela { get; set; }    
        public decimal? disponible { get; set; } //TODO disponible - campo nuevo
        public int? idcliente { get; set; } //TODO idcliente - campo nuevo
        public virtual ICollection<DevolucionDetalle> Detalles { get; set; }
    }
}

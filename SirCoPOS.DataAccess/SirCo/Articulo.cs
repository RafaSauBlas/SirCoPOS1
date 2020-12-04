using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.DataAccess.SirCo
{
    [Table("articulosest", Schema = "dbo")]
    public class Articulo
    {
        [Key]
        [Column("idarticulo")]
        public int Id { get; set; }
        [Column("marca")]
        public string Marca { get; set; }
        [Column("modelo")]
        public string Modelo { get; set; }
        public string estilof { get; set; }
        [Column("descripc")]
        public string Descripcion { get; set; }
        public int iddivisiones { get; set; }
        public string division { get; set; }
        public int idagrupacion { get; set; }
        public virtual ICollection<Serie> Series { get; set; }
        public virtual ICollection<Corrida> Corridas { get; set; }
    }
}

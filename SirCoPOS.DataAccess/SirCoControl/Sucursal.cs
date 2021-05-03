using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.DataAccess.SirCoControl
{
    [Table("sucursal", Schema = "dbo")]
    public class Sucursal
    {
        [Key]
        [Column(Order = 0)]
        public int idsucursal { get; set; }
        [Key]
        [Column(Order = 1)]
        public string sucursal { get; set; }
        public string descrip { get; set; }
        public string calle { get; set; }
        public string colonia { get; set; }
        public string ciudad { get; set; }
        public string estado { get; set; }
        public string codpostal { get; set; }
        public string visible { get; set; }
        public int? cajas { get; set; }
        public int? devolvta { get; set; }
        public string venta { get; set; }
        public int? abonos { get; set; } //TODO abonos nuevo campo
        public int? clientes { get; set; }
        public int? cvale { get; set; }
        public int idplaza { get; set; }
        public bool? web { get; set; } //TODO web nuevo campo
        public byte? ordenweb { get; set; } //TODO ordenweb nuevo campo
        [ForeignKey("idplaza")]
        public virtual Plaza Plaza { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.DataAccess.SirCoPOS
{
    [Table("Caja", Schema = "dbo")]
    public class Caja
    {
        [Key]
        [Column(Order = 0)]
        public string Sucursal { get; set; }
        [Key]
        [Column(Order = 1)]
        public byte Numero { get; set; }
        public decimal Disponible { get; set; }
        public int? ResponsableId { get; set; }
        public Common.Constants.TipoFondo Tipo { get; set; }
        public virtual ICollection<Fondo> Fondos { get; set; }
        public virtual ICollection<CajaFormaPago> FormasPago { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.DataAccess.SirCoPV
{
    [Table("agrupacionesdet", Schema = "dbo")]
    public class AgrupacionesDetalle
    {
        [Key]
        [Column(Order = 0)]
        public int idagrupacion { get; set; }
        [Key]
        [Column(Order = 1)]
        public int iddivisiones { get; set; }
        [Key]
        [Column(Order = 2)]
        public int iddepto { get; set; }
        [Key]
        [Column(Order = 3)]
        public int idfamilia { get; set; }
        [Key]
        [Column(Order = 4)]
        public int idlinea { get; set; }
        [Key]
        [Column(Order = 5)]
        public int idl1 { get; set; }
        [Key]
        [Column(Order = 6)]
        public int idl2 { get; set; }
        [Key]
        [Column(Order = 7)]
        public int idl3 { get; set; }
        [Key]
        [Column(Order = 8)]
        public int idl4 { get; set; }
        [Key]
        [Column(Order = 9)]
        public int idl5 { get; set; }
        [Key]
        [Column(Order = 10)]
        public int idl6 { get; set; }
        [Key]
        [Column(Order = 11)]
        public string marca { get; set; }
        [Key]
        [Column(Order = 12)]
        public string estilon { get; set; }
        public string nivel { get; set; }
        public int? renglon { get; set; }
        [ForeignKey("idagrupacion")]
        public virtual Agrupaciones Agrupacion { get; set; }
    }
}

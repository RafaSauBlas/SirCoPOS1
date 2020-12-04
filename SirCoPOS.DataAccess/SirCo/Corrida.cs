using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.DataAccess.SirCo
{
    [Table("corrida", Schema = "dbo")]
    public class Corrida
    {
        [Key]
        [Column("idarticulo", Order = 0)]
        public int ArticuloId { get; set; }
        [Key]
        [Column(Order = 1)]
        public string marca { get; set; }
        [Key]
        [Column(Order = 2)]
        public string proveedor { get; set; }
        [Key]
        [Column(Order = 3)]
        public string estilon { get; set; }
        [Key]
        [Column(Order = 4)]
        public string corrida { get; set; }
        [Key]
        [Column(Order = 5)]
        public int iddivisiones { get; set; }
        [Key]
        [Column(Order = 6)]
        public int iddepto { get; set; }
        [Key]
        [Column(Order = 7)]
        public int idfamilia { get; set; }
        [Key]
        [Column(Order = 8)]
        public int idlinea { get; set; }
        [Key]
        [Column(Order = 9)]
        public int idl1 { get; set; }
        [Key]
        [Column(Order = 10)]
        public int idl2 { get; set; }
        [Key]
        [Column(Order = 11)]
        public int idl3 { get; set; }
        [Key]
        [Column(Order = 12)]
        public int idl4 { get; set; }
        [Key]
        [Column(Order = 13)]
        public int idl5 { get; set; }
        [Key]
        [Column(Order = 14)]
        public int idl6 { get; set; }
        public string medini { get; set; }
        public string medfin { get; set; }
        public decimal? precio { get; set; }
        public decimal? costomargen { get; set; }
        public DateTime? ult_cmp { get; set; }
        public DateTime? ult_vta { get; set; }

        [ForeignKey("ArticuloId")]
        public virtual Articulo Articulo { get; set; }
    }
}

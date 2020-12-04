using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.DataAccess.SirCo
{
    [Table("serie", Schema = "dbo")]
    public class Serie
    {
        [Key]
        [Column(Order = 0)]
        public string serie { get; set; }
        //[Key]
        [Column(Order = 1)]
        public string sucursal { get; set; }
        //[Key]
        [Column(Order = 2)]
        public string status { get; set; }
        //[Key]
        [Column(Order = 3)]
        public string marca { get; set; }
        //[Key]
        [Column(Order = 4)]
        public string estilon { get; set; }
        //[Key]
        [Column(Order = 5)]
        public string medida { get; set; }
        //[Key]
        [Column(Order = 6)]
        public string sucurdes { get; set; }
        //[Key]
        [Column(Order = 7)]
        public int idfolio { get; set; }
        //[Key]
        [Column("idarticulo", Order = 8)]
        public int ArticuloId { get; set; }
        public decimal preciovta { get; set; }
        public decimal? costomargens { get; set; }
        public string proveedors { get; set; }
        public int? idusuariocaja { get; set; } //TODO idusuariocaja - nuevo campo
        public DateTime? fechacaja { get; set; }
        [ForeignKey("ArticuloId")]
        public virtual Articulo Articulo { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.DataAccess.SirCoImg
{
    [Table("articulosimg", Schema = "dbo")]
    public class ArticuloImg
    {        
        [Key]
        [Column(Order = 0)]
	    public string Marca { get; set; }
        [Key]
        [Column(Order = 1)]
        public string Estilon { get; set; }
	    public byte[] Foto { get; set; }
    }
}

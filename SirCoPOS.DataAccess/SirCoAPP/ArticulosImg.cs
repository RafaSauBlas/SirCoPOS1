using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.DataAccess.SirCoAPP
{
    [Table("articulosimg", Schema = "dbo")]
    public class ArticulosImg
    {
        [Key]
        public int Id { get; set; }

        public string Marca { get; set; }
        public string Estilon { get; set; }
        public byte[] Foto { get; set; }
    }
}

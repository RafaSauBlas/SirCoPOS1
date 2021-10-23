using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.DataAccess.SirCoPV
{
    [Table("seriepromocion", Schema = "dbo")]
    public class SeriePromocion
    {
        [Key]
        public string serie { get; set; }
        public decimal preciovta { get; set; }
        public string usuario { get; set; }
        public DateTime? fum { get; set; }
        
    }
}

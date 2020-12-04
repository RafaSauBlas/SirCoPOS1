using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.DataAccess.SirCo
{
    [Table("det_gto", Schema = "dbo")]
    public class DetalleGasto
    {
        [Key]
        public int idgasto { get; set; }
        public string descrip { get; set; }
    }
}

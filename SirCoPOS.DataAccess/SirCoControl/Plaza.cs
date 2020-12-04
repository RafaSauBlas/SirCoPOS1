using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.DataAccess.SirCoControl
{
    [Table("plaza", Schema = "dbo")]
    public class Plaza
    {
        [Key]
        public int idplaza { get; set; }
	    public string plaza { get; set; }
	    public string descrip { get; set; }
        public virtual ICollection<Sucursal> Sucursales { get; set; }
    }
}

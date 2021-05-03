using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.DataAccess.SirCoControl
{
    [Table("ticketmsj", Schema = "dbo")]
    public class TicketMsj
    {
        [Key]
        public short Orden { get; set; }
        public string Renglon { get; set; }
    }
}

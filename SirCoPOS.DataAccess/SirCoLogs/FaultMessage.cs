using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.DataAccess.SirCoLogs
{
    [Table("FaultMessage", Schema = "dbo")]
    public class FaultMessage
    {
        [Key]
        public Guid Id { get; set; }
        public string Action { get; set; }
        public DateTime StartDate { get; set; }
        public string Request { get; set; }
        public DateTime? EndDate { get; set; }
        public string Response { get; set; }
    }
}

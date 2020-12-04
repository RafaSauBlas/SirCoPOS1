using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.DataAccess.SirCoLogs
{
    [Table("Log", Schema = "dbo")]
    public class Log
    {
        [Key]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Application { get; set; }
	    public string Message { get; set; }
        public string Exception { get; set; }
        public string MachineName { get; set; }
        public string Level { get; set; }
        public string Logger { get; set; }
        public string Callsite { get; set; }
        public string Type { get; set; }
        public string Module { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.DataAccess.SirCoLogs
{
    [Table("Message", Schema = "dbo")]
    public class Message
    {
        [Key]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Data { get; set; }
        public Guid GID { get; set; }
        public string Action { get; set; }
        public bool IsRequest { get; set; }
        public int? UserId { get; set; }
        public string Sucursal { get; set; }
        public string Machine { get; set; }
    }
}

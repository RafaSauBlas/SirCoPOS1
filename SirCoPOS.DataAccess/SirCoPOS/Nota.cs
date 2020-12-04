using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.DataAccess.SirCoPOS
{
    [Table("Nota", Schema = "dbo")]
    public class Nota
    {
        [Key]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Sucursal { get; set; }
        public string Venta { get; set; }
        public int CajeroId { get; set; }
        public int VendedorId { get; set; }
        public string Data { get; set; }
        public bool Multiple { get; set; }
        public virtual ICollection<NotaDetalle> Items { get; set; }
        public virtual ICollection<NotaPago> Pagos { get; set; }
    }
}

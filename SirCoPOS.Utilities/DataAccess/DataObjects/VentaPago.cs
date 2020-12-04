using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Utilities.DataAccess.DataObjects
{
    public class VentaPago
    {
        [Key]
        public Guid Id { get; set; }
        public Guid VentaId { get; set; }
        public Common.Constants.FormaPago FormaPago { get; set; }
        public decimal? Importe { get; set; }    
        public string Data { get; set; }
        public virtual Venta Venta { get; set; }
    }
}

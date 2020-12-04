using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SirCoPOS.Web.Models
{
    public class Pago
    {
        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; }
        [DataType(DataType.Currency)]
        public decimal Subtotal { get; set; }
        [DataType(DataType.Currency)]
        public decimal Descuento { get; set; }
        [DataType(DataType.Currency)]
        public decimal Importe { get; set; }
    }
}
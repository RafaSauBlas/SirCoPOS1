using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SirCoPOS.Web.Models
{
    public class SolicitudCliente
    {
        public string Vale { get; set; }
        public int Id { get; set; }
        public string Cliente { get; set; }
        public string ApPaterno { get; set; }
        public string ApMaterno { get; set; }
        public string Nombre { get; set; }
        [DataType(DataType.Currency)]
        public decimal MontoVale { get; set; }
        [DataType(DataType.Currency)]
        public decimal Monto { get; set; }
        [DataType(DataType.Currency)]
        public decimal Faltante { get; set; }
        public bool Electronica { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SirCoPOS.Web.Models
{
    public class Distribuidor
    {
        public int Id { get; set; }
        public string Cuenta { get; set; }
        public string Nombre { get; set; }
        public string ApPaterno { get; set; }
        public string ApMaterno { get; set; }
        public Common.Constants.StatusDistribuidor Status { get; set; }
        public bool Electronica { get; set; }
        public IEnumerable<short> Firmas { get; set; }
        public bool ContraVale { get; set; }
        public bool Promocion { get; set; }
        [DataType(DataType.Currency)]
        public decimal? Disponible { get; set; }
        public short Plazos { get; set; }
        public DateTime? Date { get; set; }
        public DateTime? PrimerPago { get; set; }
        public string Tipo { get; set; }
        [DataType(DataType.Currency)]
        public decimal? LimiteVale { get; set; }
        [DataType(DataType.Currency)]
        public decimal? LimiteCredito { get; set; }
    }
}
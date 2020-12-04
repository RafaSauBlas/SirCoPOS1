using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Reports.Entities
{
    public class ReciboPago
    {
        public string Sucursal { get; set; }
        public string Folio { get; set; }
        public DateTime Fecha { get; set; }
        public string ClienteId { get; set; }
        public string ClienteNombre { get; set; }
        public decimal Cantidad { get; set; }
        public string CantidadLetra { get; set; }
        public string Concepto { get; set; }
        public decimal Descuento { get; set; }
        public decimal Vencido { get; set; }
        public string Recibido { get; set; }
        public decimal Disponible { get; set; }
        public decimal Saldo { get; set; }
        public int CajeroId { get; set; }
        public int? VendedorId { get; set; }
        public string CajeroNombre { get; set; }
        public string VendedorNombre { get; set; }
    }
}

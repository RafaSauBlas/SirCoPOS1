using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Common.Entities
{
    public class FondoRequest
    {
        public decimal Importe { get; set; }
        public int Auditor { get; set; }
        public int Responsable { get; set; }
        public string Sucursal { get; set; }
        public byte Numero { get; set; }
        public Common.Constants.TipoFondo Tipo { get; set; }
    }
    public class FondoArqueoRequest
    {
        public decimal Importe { get; set; }
        public decimal? Entregar { get; set; }
        public int Auditor { get; set; }
        public int Responsable { get; set; }
    }
    public class FondoTransferRequest
    {
        public decimal Importe { get; set; }
        public int UserFrom { get; set; }
        public int UserTo { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Reports.Entities
{
    public class ContraVale
    {
        public string Folio { get; set; }
        public DateTime Fecha { get; set; }
        public string Nota { get; set; }
        public DateTime NotaFecha { get; set; }
        public DateTime Vencimiento { get; set; }
        public decimal ValorMaximo { get; set; }
        public string RFC { get; set; }
        public string SucId { get; set; }
        public string SucNombre { get; set; }
        public string SucDireccion { get; set; }
        public string SucColonia { get; set; }
        public string SucCiudad { get; set; }
        public string SucEstado { get; set; }
        public string SucCodPostal { get; set; }
        public string VendedorId { get; set; }
        public string VendedorNombre { get; set; }
        public string CajeroId { get; set; }
        public string CajeroNombre { get; set; }
        public string ClienteId { get; set; }
        public string ClienteSuc { get; set; }
        public string ClienteNombre { get; set; }
        public string ClienteDireccion { get; set; }
        public string ClienteColonia { get; set; }
        public string ClienteCiudad { get; set; }
        public string ClienteEstado { get; set; }
        public string ClienteCodPostal { get; set; }
        public string Distribuidor { get; set; }
        public string DistribuidorNombre { get; set; }

    }
}

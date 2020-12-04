using SirCoPOS.Common.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Common.Entities
{
    public class SaleRequest : CheckPromocionesCuponesRequest
    {
        public SaleType Tipo { get; set; }
        public int? VendedorId { get; set; }
        public IEnumerable<Pago> Pagos { get; set; }
        public Cliente Cliente { get; set; }
    }

    public enum SaleType
    { 
        Sale,
        Change,
        Note
    }

    public class Pago
    {
        public FormaPago FormaPago { get; set; }
        public decimal Importe { get; set; }
        //tarjeta
        public string Terminacion { get; set; }
        public string Referencia { get; set; }
        //devolucion
        public string Sucursal { get; set; }
        public string Devolucion { get; set; }
        //vale
        public string Vale { get; set; }
        public int? Plazos { get; set; }
        public DateTime? FechaAplicar { get; set; }
        public bool ContraVale { get; set; }
        public decimal? Limite { get; set; }
        public IEnumerable<ProductoPlazo> ProductosPlazos { get; set; }
        //credito
        public string Distribuidor { get; set; }

        public string NoCuenta { get; set; }
        public int? Negocio { get; set; }
    }
    public class ProductoPlazo
    {
        public string Serie { get; set; }
        public int? Plazos { get; set; }
        public decimal? Importe { get; set; }
    }
}

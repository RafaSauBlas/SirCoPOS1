using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Common.Entities
{
    public class Producto
    {
        public int? Id { get; set; }
        public string Serie { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Talla { get; set; }
        public decimal? Precio { get; set; } 
        public decimal? Pago { get; set; }

        public decimal? Total { get; set; }
        
        public string Corrida { get; set; }
        
        public bool HasImage { get; set; }
        public bool Electronica { get; set; }
        public bool Accesorio { get; set; }
        public bool ParUnico { get; set; }
        public byte? MaxPlazos { get; set; }        
        public string Sucursal { get; set; }
    }

    public class ProductoSale : Producto
    {
        //public decimal? Descuento { get; set; }
        public short Renglon { get; set; }
        public decimal? precdesc { get; set; }
        public decimal? costomargen { get; set; }
        public decimal? iva { get; set; }
        public short? ctd { get; set; }
        public int? idpromocion { get; set; }
    }
}

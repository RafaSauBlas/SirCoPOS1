using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SirCoPOS.Web.Areas.Admin.Models
{
    public class Producto
    {
        public short Renglon { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Serie { get; set; }
        public decimal? Precio { get; set; }
        public decimal? Pago { get; set; }
        public string Comments { get; set; }
        public bool HasImage { get; set; }
        public decimal? DescuentoPorcentaje
        {
            get
            {
                if (this.Precio != this.Pago)
                {
                    var desc = this.Precio - this.Pago;
                    var per = desc / this.Precio;
                    return per;
                }
                return null;
            }
        }
        public string Notas { get; set; }
        public string NotaRazon { get; set; }
    }
}
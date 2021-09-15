using SirCoPOS.Common.Constants;
using SirCoPOS.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SirCoPOS.Utilities.Messages
{
    public class Pago : ModalResponse
    {
        public FormaPago FormaPago { get; set; }
        public decimal Importe { get; set; }
        public decimal Efectivo { get; set; }
        public string Sucursal { get; set; }
        public string Folio { get; set; }
        public string Terminacion { get; set; }
        public string Referencia { get; set; }
        public string Vale { get; set; }
        public int? Cliente { get; set; }
        [XmlIgnore]
        public IEnumerable<int> Plazos { get; set; }
        public int? SelectedPlazo { get; set; }
        [XmlIgnore]
        public IEnumerable<DateTime> Promociones { get; set; }
        public DateTime? SelectedPromocion { get; set; }
        public bool ContraVale { get; set; }
        public decimal? Limite { get; set; }
        public int? DistribuidorId { get; set; }
        public ProductoPlazo[] PlazosProductos { get; set; }

        public string NoCuenta { get; set; }
        public int? Negocio { get; set; }
        public string NombreCliente { get; set; }
        public string TipoDev { get; set; }
    }
}

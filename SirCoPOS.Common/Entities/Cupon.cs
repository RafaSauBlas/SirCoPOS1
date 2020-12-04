using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Common.Entities
{
    public class Cupon
    {
        public string Folio { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Restricciones { get; set; }
        public IEnumerable<PromocionCupon> Promociones { get; set; }
        public CuponStatus Status { get; set; }
    }
    public enum CuponStatus
    {
        Expirado,
        Inactivo,
        Activo
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.DataAccess.SirCoPOS
{
    [Table("Fondo", Schema = "dbo")]
    public class Fondo
    {
        [Key]
        public int Id { get; set; }
        public int ResponsableId { get; set; }
        public DateTime FechaApertura { get; set; }
        //public decimal ImporteApertura { get; set; }
        //public decimal? ImporteCierre { get; set; }
        public decimal Disponible { get; set; }
        public Common.Constants.TipoFondo Tipo { get; set; }
        public int AuditorAperturaId { get; set; }
        public int? AuditorCierreId { get; set; }
        public DateTime? FechaCierre { get; set; }
        public string CajaSucursal { get; set; }
        public byte? CajaNumero { get; set; }
        public virtual Caja Caja { get; set; }

        public virtual ICollection<FondoMovimiento> Movimientos { get; set; }
        public virtual ICollection<FondoArqueo> Arqueos { get; set; }        
        public virtual ICollection<FondoFormaPago> FormasPago { get; set; }        
    }
}

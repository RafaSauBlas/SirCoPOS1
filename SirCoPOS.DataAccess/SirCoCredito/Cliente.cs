using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.DataAccess.SirCoCredito
{
    [Table("cliente", Schema = "dbo")]
    public class Cliente
    {
        [Key]
        [Column(Order = 0)]
        public int idcliente { get; set; }
	    public int? idsucursal { get; set; }
        public string cliente { get; set; }
        public string nombrecompleto { get; set; }
        public string nombre { get; set; }
        public string appaterno { get; set; }
        public string apmaterno { get; set; }
        public string sexo { get; set; }
        public int? idestado { get; set; }
        public int? idciudad { get; set; }
        public int? idcolonia { get; set; }
        public string codigopostal { get; set; }
        public string calle { get; set; }
        public short numero { get; set; }
        public string celular1 { get; set; }
        public string email { get; set; }
        public DateTime? fecalta { get; set; }
        public int? idusuario { get; set; }
        public DateTime? fum { get; set; }
        public int? idusuariomodif { get; set; }
        public DateTime? fummodif { get; set; }
        public string sistema { get; set; }
        public byte[] foto { get; set; } //TODO foto - nuevo campo
        public string celular { get; set; } //TODO celular - nuevo campo
        //public int? idempleado { get; set; } // idempleado - nuevo campo
        public string identificacion { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.DataAccess.SirCoCredito
{
    [Table("distrib", Schema = "dbo")]
    public class Distribuidor
    {
        [Key]
        [Column(Order = 0)]
        public int iddistrib { get; set; }
        [Key]
        [Column(Order = 1)]
        public int idpromotor { get; set; }
        [Key]
        [Column(Order = 2)]
        public int idcoordinador { get; set; }
        [Key]
        [Column(Order = 3)]
        public int idtienda { get; set; }
        [Key]
        [Column(Order = 4)]
        public string tipodistrib { get; set; }
        [Key]
        [Column(Order = 5)]
        public string distrib { get; set; }
        [Key]
        [Column(Order = 6)]
        public short idtipocredito { get; set; }
        public Common.Constants.StatusDistribuidor? idestatus { get; set; }
        public string nombrecompleto { get; set; }
        public string nombre { get; set; }
        public string appaterno { get; set; }
        public string apmaterno { get; set; }        
        public decimal? limitecredito { get; set; }
        public decimal? saldo { get; set; }
        public decimal? disponible { get; set; }
        public decimal? limitevale { get; set; }
        public string clasificacion { get; set; }
        public int desctoori { get; set; }
        public string observ01 { get; set; }
        public string observ02 { get; set; }
        public string observ03 { get; set; }
        public string observ04 { get; set; }
        public string observ05 { get; set; }
        public short? solocalzado { get; set; }
        public short? idperiodicidad { get; set; }
        public short? contravale { get; set; }
        public short? negext { get; set; }
        public short? promocion { get; set; }
        public string succtedi { get; set; }
        public string clientedi { get; set; }

        public string sexo { get; set; }
        public string celular1 { get; set; }
        public int? idestado { get; set; }
	    public int? idciudad { get; set; }
	    public int? idcolonia { get; set; }
	    public int? codigopostal { get; set; }
	    public string calle { get; set; }
	    public short numero { get; set; }
        public string email { get; set; }
        public DateTime? fum { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Common.Entities
{
    public class Empleado
    {
        public int Id { get; set; }
        public string Clave { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Nombre { get; set; }
        public string Usuario { get; set; }
        public int Puesto { get; set; }
        public int Depto { get; set; }
        public decimal? Disponible { get; set; }
        public string Sucursal { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Common.Entities
{
    public class Colonia
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string CodigoPostal { get; set; }
        public int CiudadId { get; set; }
        public string CiudadNombre { get; set; }
        public int EstadoId { get; set; }
        public string EstadoNombre { get; set; }
    }
}

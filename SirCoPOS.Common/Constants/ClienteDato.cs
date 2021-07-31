using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Common.Constants
{
    public class ClienteDato
    {
        public int? Id { get; set; }
        public string Nombre { get; set; }
        public string ApPaterno { get; set; }
        public string ApMaterno { get; set; }
        public string Celular { get; set; }
        public string Celular1 { get; set; }
        public string CodigoPostal { get; set; }
        public int? Colonia { get; set; }
        public int? Ciudad { get; set; }
        public int? Estado { get; set; }
        public string Calle { get; set; }
        public short Numero { get; set; }
        public string Referencia { get; set; }
        public string Email { get; set; }
        public string Sexo { get; set; }
        public string NombreCompleto { get; set; }
    }
}

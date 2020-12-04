using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Utilities.Entities
{
    public class NuevoCliente
    {
        public string Nombre { get; set; }
        public string ApPaterno { get; set; }
        public string ApMaterno { get; set; }
        public string CodigoPostal { get; set; }
        public Common.Entities.Colonia Colonia { get; set; }
        public string Celular { get; set; }
        public string Calle { get; set; }
        public short? Numero { get; set; }
        public string Referencia { get; set; }
        public string Email { get; set; }
        public string Sexo { get; set; }
    }
}

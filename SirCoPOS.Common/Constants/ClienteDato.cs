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


        public static int id { get; set; } = 0;
        public static string nombre { get; set; } = string.Empty;
        public static string appa { get; set; } = string.Empty;
        public static string apma { get; set; } = string.Empty;
        public static string cp { get; set; } = string.Empty;
        public static int estado { get; set; } = 0;
        public static int ciudad { get; set; } = 0;
        public static string colonia { get; set; } = string.Empty;
        public static string calle { get; set; } = string.Empty;
        public static short numero { get; set; } = 0;
        public static string referencia { get; set; } = string.Empty;
        public static string celular { get; set; } = string.Empty;
        public static string celular1 { get; set; } = string.Empty;
        public static string email { get; set; } = string.Empty;
        public static string sexo { get; set; } = string.Empty;
        public static int opcion = 0;
        public static string error { get; set; } = string.Empty;
        public static string identif { get; set; } = string.Empty;

    }
}

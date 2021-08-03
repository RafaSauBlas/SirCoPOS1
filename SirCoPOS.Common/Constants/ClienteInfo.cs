using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Common.Constants
{
    public class ClienteInfo
    {
        public static string nombre { get; set; } = string.Empty;
        public static string appa { get; set; } = string.Empty;
        public static string apma { get; set; } = string.Empty;
        public static string cp { get; set; } = string.Empty;
        public static string estado { get; set; } = string.Empty;
        public static string ciudad { get; set; } = string.Empty;
        public static int colonia { get; set; } = 0;
        public static string colonianame { get; set; } = string.Empty;
        public static string calle { get; set; } = string.Empty;
        public static string numero { get; set; } = string.Empty;
        public static string referencia { get; set; } = string.Empty;
        public static string celular { get; set; } = string.Empty;
        public static string email { get; set; } = string.Empty;
        public static string sexo { get; set; } = string.Empty;
        public static IEnumerable<Common.Entities.Colonia> Colonias { get; set; }
        public static int opcion = 0;
    }
}

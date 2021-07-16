using SirCoPOS.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Win.Helpers
{
    public class Usuario
    {
        public static bool Completar(string dato, int tamaño)
        {
            if (dato == null) return false;
            if (dato.Length == tamaño)
            {
                return true;
            }
            return false;
        }
    }
}

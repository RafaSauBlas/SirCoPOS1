using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Common.Helpers
{
    public static class TypeHelper
    {
        public static bool IsDefault<T>(this T val)
        {
            return EqualityComparer<T>.Default.Equals(val, default(T));
        }

        public static bool IsEquals<T>(this T val, T compare)
        {
            return EqualityComparer<T>.Default.Equals(val, compare);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SirCoPOS.App.Helpers
{
    class CommonHelper
    {
        public string PrepareSerie(string serie)
        {
            return this.Prepare(serie, 13);
        }
        private string Prepare(string code, int len)
        {
            if (string.IsNullOrWhiteSpace(code))
                return null;
            if (code.Length != len)
            {
                code.Trim().TrimStart('0');
                int ncode;
                if (!int.TryParse(code, out ncode))
                    return null;
                var format = "{0:" + string.Concat(Enumerable.Repeat("0", len)) + "}";
                code = String.Format(format, ncode);
            }
            return code;
        }
    }
}

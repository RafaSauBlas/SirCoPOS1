using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SirCoPOS.Client.Helpers
{
    public class CommonHelper
    {
        public string PrepareTarjetahabiente(string id)
        {
            return this.Prepare(id, 6);
        }
        public string PrepareCupon(string cupon)
        {
            return this.Prepare(cupon, 10);
        }
        public string PrepareSerie(string serie)
        {
            return this.Prepare(serie, 13);
        }
        public string PrepareModelo(string estilon)
        {
            return this.Prepare(estilon, 7, ' ');
        }
        public string PrepareContraVale(string code)
        {
            return this.Prepare(code, 6);
        }
        public string PrepareVentaDevolucion(string code)
        {
            return this.Prepare(code, 6);
        }
        private string Prepare(string code, int len, char c = '0')
        {
            if (string.IsNullOrWhiteSpace(code))
                return null;
            //if (code.Length != len)
            //{
            //    code.Trim().TrimStart('0');
            //    int ncode;
            //    if (!int.TryParse(code, out ncode))
            //        return null;
            //    var format = "{0:" + string.Concat(Enumerable.Repeat("0", len)) + "}";
            //    code = String.Format(format, ncode);
            //}
            //return code;
            return code.PadLeft(len, c);
        }

        public string PreparePhone(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone))
                return null;
            return Regex.Replace(phone, @"^(\+)|\D", "");
        }

        public string PrepareNombre(string nombre)
        {
            return nombre;
        }
    }
}

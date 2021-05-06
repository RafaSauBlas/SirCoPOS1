using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Common.Helpers
{
    public class CommonHelper
    {
        public Tuple<decimal, decimal, int> GetPlazos(decimal importe, int plazos)
        {
            var plazoent = plazos;
            var part = importe / plazos;
            var pagos = Math.Ceiling(part);
            var restante = pagos * (plazos - 1);
            var faltante = importe - restante;
            int j = 2;

            while (faltante < 0)
            {
                restante = pagos * (plazos - j);
                faltante = importe - restante;
                plazoent = plazos - (j - 1);
                j++;
            }

            return new Tuple<decimal, decimal, int>(pagos, faltante, plazoent);
        }

        public IDictionary<DateTime, decimal> GetPlazos(DateTime[] fechas, params Tuple<int, decimal>[] ip)
        {
            var dic = new Dictionary<int, decimal>();
            var exp = new List<decimal[]>();
            foreach (var t in ip)
            {
                if (!dic.ContainsKey(t.Item1))
                    dic.Add(t.Item1, 0);
                dic[t.Item1] += t.Item2;
            }
            var max = dic.Keys.Max();
            foreach (var item in dic)
            {
                var plazos = this.GetPlazos(item.Value, item.Key);
                var pagos = new decimal[item.Key];
                for (int i = 0; i < item.Key; i++)
                {
                    if (item.Key == plazos.Item3 || (i + 1) < plazos.Item3) { 
                        pagos[i] = (i + 1) == item.Key ? plazos.Item2 : plazos.Item1;
                    } else
                    {
                        pagos[i] = (i + 1) > plazos.Item3 ?  0 : plazos.Item2;
                    }
                }
                exp.Add(pagos);
            }
             
            var res = new decimal[max];
            for (int i = 0; i < max; i++)
            {
                foreach (var item in exp)
                {
                    if (i < item.Length)
                        res[i] += item[i];
                }
            }
            var resFechas = new Dictionary<DateTime, decimal>();
            for (int i = 0; i < res.Length; i++)
            {
                resFechas.Add(fechas[i], res[i]);
            }
            return resFechas;
        }
    }
}

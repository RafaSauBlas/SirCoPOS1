using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.BusinessLogic.Helpers
{
    public class Combinations<T> where T : class
    {
        public List<List<T>> Generate(T[] items, int max, decimal importe, Func<T, decimal> getImporte)
        {
            var res = new List<List<T>>();
            Generate(res, max, new List<T>(), items, importe, getImporte);
            return res;
        }
        private void Generate(List<List<T>> res, int max, List<T> list, IEnumerable<T> items, 
            decimal importe, Func<T, decimal> getImporte)
        {
            if (max > 0 && list.Count == max)
            {
                res.Add(list);
                return;
            }
            if (importe > 0 && list.Sum(i => getImporte(i)) >= importe)
            {
                res.Add(list);
                return;
            }

            if (!items.Any())
                return;
            
            foreach (var item in items)
            {
                var nlist = list.ToList();
                nlist.Add(item);
                Generate(res, max, nlist, items.Where(i => i != item), importe, getImporte);
            }
        }
    }
}

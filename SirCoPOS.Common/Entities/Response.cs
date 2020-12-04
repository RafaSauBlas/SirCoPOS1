using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Common.Entities
{
    public class Response
    {
        public bool Success { get; set; }
        public string Error { get; set; }        
    }
    public class Response<T> : Response
    {
        public T Item { get; set; }
    }
}

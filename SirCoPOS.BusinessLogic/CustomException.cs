using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.BusinessLogic
{
    public class CustomException : ApplicationException
    {
        public CustomException(string msg)
            : base(msg)
        {
            if (String.IsNullOrEmpty(msg))
                throw new NotSupportedException();
        }
    }
}

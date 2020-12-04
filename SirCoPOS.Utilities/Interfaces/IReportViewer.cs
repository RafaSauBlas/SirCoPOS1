using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Utilities.Interfaces
{
    public interface IReportViewer
    {
        void OpenViewer(string fullname, string library,
            IDictionary<string, IEnumerable<object>> datasources = null,
            IDictionary<string, string> parameters = null);
    }
}

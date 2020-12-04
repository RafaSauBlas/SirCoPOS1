using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Win.Helpers
{
    class ReportViewer : Utilities.Interfaces.IReportViewer
    {
        public void OpenViewer(string fullname, string library, 
            IDictionary<string, IEnumerable<object>> datasources = null, 
            IDictionary<string, string> parameters = null)
        {
            var win = new Windows.ViewerWindow(
                fullname: fullname,
                library: library,
                datasources: datasources, 
                parameters: parameters);
            win.Show();
        }
    }
}

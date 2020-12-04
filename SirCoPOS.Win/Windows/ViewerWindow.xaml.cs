using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SirCoPOS.Win.Windows
{
    /// <summary>
    /// Interaction logic for Viewer.xaml
    /// </summary>
    public partial class ViewerWindow : Window
    {
        public ViewerWindow()
        {
            InitializeComponent();
        }

        private string _fullname, _library;
        private IDictionary<string, IEnumerable<object>> _datasources;
        private IDictionary<string, string> _parameters;

        public ViewerWindow(string fullname, string library, 
            IDictionary<string, IEnumerable<object>> datasources = null, 
            IDictionary<string, string> parameters = null)
            : this()
        {
            _fullname = fullname;
            _library = library;
            _datasources = datasources;
            _parameters = parameters;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //this.reportViewer.LocalReport.ReportEmbeddedResource = _report;
            var stream = Helpers.PrintFile.GetReportStreamToLoad(_fullname, _library);            
            this.reportViewer.LocalReport.LoadReportDefinition(stream);
            if (_datasources != null)
            {
                foreach (var item in _datasources)
                {
                    this.reportViewer.LocalReport.DataSources.Add(
                        new Microsoft.Reporting.WinForms.ReportDataSource(item.Key, item.Value));
                }
            }
            if (_parameters != null)
            {
                foreach (var item in _parameters)
                {
                    this.reportViewer.LocalReport.SetParameters(
                        new Microsoft.Reporting.WinForms.ReportParameter(item.Key, item.Value));
                }                
            }
            this.reportViewer.RefreshReport();
        }
    }
}

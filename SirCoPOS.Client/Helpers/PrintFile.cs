using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Client.Helpers
{
    class PrintFile
    {
        private LocalReport report;
        //https://docs.microsoft.com/en-us/previous-versions/ms252091(v=vs.140)
        private List<Stream> m_streams = null;
        private int m_currentPageIndex;
        public static Stream GetReportStreamToLoad(string fullname, string library)
        {
            var asm = System.Reflection.Assembly.Load(library);
            var stream = asm.GetManifestResourceStream(fullname);
            //stream = TranslateReport(stream);.
            return stream;
        }
        public PrintFile(string fullname, string library, IDictionary<string, IEnumerable<object>> datasources)
        {
            report = new LocalReport();

            var stream = GetReportStreamToLoad(fullname, library);
            report.LoadReportDefinition(stream);
            foreach (var ds in datasources)
            {
                report.DataSources.Add(new ReportDataSource(ds.Key, ds.Value));
            }
            Export(report);
        }
        private void Export(LocalReport report)
        {
            string deviceInfo =
            @"<DeviceInfo>
                <OutputFormat>EMF</OutputFormat>                
            </DeviceInfo>";
            Warning[] warnings;
            m_streams = new List<Stream>();
            var ps = report.GetDefaultPageSettings();
            report.Render("Image", deviceInfo, CreateStream, out warnings);
            foreach (Stream stream in m_streams)
                stream.Position = 0;
        }
        private Stream CreateStream(string name,
            string fileNameExtension, Encoding encoding,
            string mimeType, bool willSeek)
        {
            Stream stream = new MemoryStream();
            m_streams.Add(stream);
            return stream;
        }
        public void Print()
        {
            if (m_streams == null || m_streams.Count == 0)
                throw new Exception("Error: no stream to print.");
            var printDoc = new PrintDocument();
            if (!printDoc.PrinterSettings.IsValid)
            {
                throw new Exception("Error: cannot find the default printer.");
            }
            else
            {
                printDoc.PrintPage += new PrintPageEventHandler(PrintPage);
                m_currentPageIndex = 0;
                printDoc.Print();
            }
        }

        private void PrintPage(object sender, PrintPageEventArgs ev)
        {
            Metafile pageImage = new
               Metafile(m_streams[m_currentPageIndex]);

            // Adjust rectangular area with printer margins.
            var adjustedRect = new System.Drawing.Rectangle(
                ev.PageBounds.Left - (int)ev.PageSettings.HardMarginX,
                ev.PageBounds.Top - (int)ev.PageSettings.HardMarginY,
                ev.PageBounds.Width,
                ev.PageBounds.Height);

            // Draw a white background for the report
            ev.Graphics.FillRectangle(System.Drawing.Brushes.White, adjustedRect);

            // Draw the report content
            ev.Graphics.DrawImage(pageImage, adjustedRect);

            // Prepare for the next page. Make sure we haven't hit the end.
            m_currentPageIndex++;
            ev.HasMorePages = (m_currentPageIndex < m_streams.Count);
        }
    }
}

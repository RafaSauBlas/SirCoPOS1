using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Win.Helpers
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
        /*
         public static void Print(this LocalReport report, PageSettings pageSettings)
    {
        string deviceInfo =
            $@"<DeviceInfo>
                <OutputFormat>EMF</OutputFormat>
                <PageWidth>{pageSettings.PaperSize.Width * 100}in</PageWidth>
                <PageHeight>{pageSettings.PaperSize.Height * 100}in</PageHeight>
                <MarginTop>{pageSettings.Margins.Top * 100}in</MarginTop>
                <MarginLeft>{pageSettings.Margins.Left * 100}in</MarginLeft>
                <MarginRight>{pageSettings.Margins.Right * 100}in</MarginRight>
                <MarginBottom>{pageSettings.Margins.Bottom * 100}in</MarginBottom>
            </DeviceInfo>";

        Warning[] warnings;
        var streams = new List<Stream>();
        var currentPageIndex = 0;

        report.Render("Image", deviceInfo, 
            (name, fileNameExtension, encoding, mimeType, willSeek) => 
            {
                var stream = new MemoryStream();
                streams.Add(stream);
                return stream;
            }, out warnings);

        foreach (Stream stream in streams)
            stream.Position = 0;

        if (streams == null || streams.Count == 0)
            throw new Exception("Error: no stream to print.");

        var printDocument = new PrintDocument();
        printDocument.DefaultPageSettings = pageSettings;
        if (!printDocument.PrinterSettings.IsValid)
            throw new Exception("Error: cannot find the default printer.");
        else
        {
            printDocument.PrintPage += (sender, e) =>
            {
                Metafile pageImage = new Metafile(streams[currentPageIndex]);
                Rectangle adjustedRect = new Rectangle(
                    e.PageBounds.Left - (int)e.PageSettings.HardMarginX,
                    e.PageBounds.Top - (int)e.PageSettings.HardMarginY,
                    e.PageBounds.Width,
                    e.PageBounds.Height);
                e.Graphics.FillRectangle(Brushes.White, adjustedRect);
                e.Graphics.DrawImage(pageImage, adjustedRect);
                currentPageIndex++;
                e.HasMorePages = (currentPageIndex < streams.Count);
                e.Graphics.DrawRectangle(Pens.Red, adjustedRect);
            };
            printDocument.EndPrint += (Sender, e) =>
            {
                if (streams != null)
                {
                    foreach (Stream stream in streams)
                        stream.Close();
                    streams = null;
                }
            };
            printDocument.Print();
        }
    }
             
             */
        private void Export(LocalReport report)
        {
            /*
             * var pageSettings = new PageSettings();
        pageSettings.PaperSize = report.GetDefaultPageSettings().PaperSize;
        pageSettings.Landscape = report.GetDefaultPageSettings().IsLandscape;
        pageSettings.Margins = report.GetDefaultPageSettings().Margins;
        Print(report, pageSettings);
             string deviceInfo =
            $@"<DeviceInfo>
                <OutputFormat>EMF</OutputFormat>
                <PageWidth>{pageSettings.PaperSize.Width * 100}in</PageWidth>
                <PageHeight>{pageSettings.PaperSize.Height * 100}in</PageHeight>
                <MarginTop>{pageSettings.Margins.Top * 100}in</MarginTop>
                <MarginLeft>{pageSettings.Margins.Left * 100}in</MarginLeft>
                <MarginRight>{pageSettings.Margins.Right * 100}in</MarginRight>
                <MarginBottom>{pageSettings.Margins.Bottom * 100}in</MarginBottom>
            </DeviceInfo>";
             */

            string deviceInfo =
            //  @"<DeviceInfo>
            //    <OutputFormat>EMF</OutputFormat>
            //    <PageWidth>7cm</PageWidth>
            //    <PageHeight>8in</PageHeight>
            //    <MarginTop>0in</MarginTop>
            //    <MarginLeft>0in</MarginLeft>
            //    <MarginRight>0in</MarginRight>
            //    <MarginBottom>0in</MarginBottom>
            //</DeviceInfo>";
            //@"<DeviceInfo>
            //    <OutputFormat>EMF</OutputFormat>
            //    <PageWidth>276</PageWidth>
            //    <PageHeight>600</PageHeight>
            //    <MarginTop>0</MarginTop>
            //    <MarginLeft>0</MarginLeft>
            //    <MarginRight>0</MarginRight>
            //    <MarginBottom>0</MarginBottom>
            //</DeviceInfo>";
            @"<DeviceInfo>
                <OutputFormat>EMF</OutputFormat>                
            </DeviceInfo>";
            Warning[] warnings;
            m_streams = new List<Stream>();
            var ps = report.GetDefaultPageSettings();
            report.Render("Image", deviceInfo, CreateStream, out warnings);
            //report.Render("Image");
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

using SirCoPOS.Common.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;

namespace SirCoPOS.Services
{
    public class SyncService : Common.ServiceContracts.ISyncService
    {
        public IEnumerable<FileSync> GetFiles()
        {
            var bp = HostingEnvironment.MapPath("~/");
            var path = $"{bp}..\\{ConfigurationManager.AppSettings["dlls"]}";
            //var path = @"C:\tmp\sync\server";
            var files = System.IO.Directory.GetFiles(path);
            var res = new List<FileSync>();
            var md5 = MD5.Create();
            foreach (var item in files)
            {
                using (var stream = File.OpenRead(item))
                {
                    var hash = md5.ComputeHash(stream);                    
                    res.Add(new FileSync
                    {
                        FileName = System.IO.Path.GetFileName(item),
                        FileSize = (int)stream.Length,
                        Date = System.IO.File.GetLastWriteTimeUtc(item),
                        Checksum = Convert.ToBase64String(hash)
                    });
                }
            }
            return res;
        }
        public Stream GetFile(string name)
        {
            var bp = HostingEnvironment.MapPath("~/");
            var path = $"{bp}..\\{ConfigurationManager.AppSettings["dlls"]}";
            var filename = System.IO.Path.Combine(path, name);
            var fs = System.IO.File.Open(filename, FileMode.Open, FileAccess.Read);
            return fs;
        }
    }
}

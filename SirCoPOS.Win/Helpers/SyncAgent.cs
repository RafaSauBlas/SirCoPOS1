using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;

namespace SirCoPOS.Win.Helpers
{
    class SyncAgent
    {
        private bool FileInUse(string name)
        {
            try
            {
                using (var fs = System.IO.File.OpenWrite(name))
                {
                    fs.Close();
                }
            }
            catch (IOException)
            {
                return true;
            }
            return false;
        }
        public bool Sync()
        {
            var res = SyncHelper();
            //if (res)
            //{
            //    var dir = @"sync";
            //    var files = System.IO.Directory.GetFiles(dir);
            //    foreach (var item in files)
            //    {
            //        var fn = System.IO.Path.GetFileName(item);
            //        System.IO.File.Copy(item, $"..\\{fn}", true);
            //    }                
            //}
            return res;
        }
        private bool SyncHelper()
        {
            try
            {
                var dir = @"sync";
                if (!System.IO.Directory.Exists(dir))
                    System.IO.Directory.CreateDirectory(dir);

                var proxy = new System.ServiceModel.ChannelFactory<Common.ServiceContracts.ISyncService>("*").CreateChannel();
                var files = proxy.GetFiles();
                var path = dir;
                var md5 = MD5.Create();
                var current = System.IO.Directory.GetFiles(path);
                foreach (var item in current)
                {
                    var name = System.IO.Path.GetFileName(item);
                    var q = files.Where(i => i.FileName == name);
                    if (!q.Any())
                    {
                        if (this.FileInUse(item))
                            return false;
                        System.IO.File.Delete(item);
                    }
                }

                foreach (var item in files)
                {
                    var fname = System.IO.Path.Combine(path, item.FileName);
                    var download = false;
                    if (!System.IO.File.Exists(fname))
                    {
                        download = true;
                    }
                    else
                    {
                        using (var stream = System.IO.File.OpenRead(fname))
                        {
                            var len = stream.Length;
                            var hash = md5.ComputeHash(stream);
                            var checksum = Convert.ToBase64String(hash);
                            stream.Close();
                            var date = System.IO.File.GetLastWriteTimeUtc(fname);
                            if (checksum != item.Checksum
                                || len != item.FileSize
                                || date != item.Date)
                            {
                                download = true;
                            }
                        }
                    }
                    if (download)
                    {
                        using (var stream = proxy.GetFile(item.FileName))
                        {
                            if (System.IO.File.Exists(fname))
                            {
                                if (this.FileInUse(fname))
                                    return false;
                                System.IO.File.Delete(fname);
                            }
                            using (var fw = new System.IO.FileStream(fname,
                                System.IO.FileMode.Create,
                                System.IO.FileAccess.Write,
                                System.IO.FileShare.None,
                                item.FileSize))
                            {
                                stream.CopyTo(fw);
                                fw.Flush();
                                fw.Close();
                            }
                        }
                        System.IO.File.SetLastWriteTimeUtc(fname, item.Date);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("A handled exception just occurred: " + ex.Message, "Exception Sample");
            }
            return true;
        }
    }
}

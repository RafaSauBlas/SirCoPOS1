using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Win.SecuGen
{
    class Loader
    {
        [DllImport("kernel32.dll", CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern IntPtr LoadLibrary(string lpLibFileName);

        public static void LoadAssemblies()
        {            
            var rootApplicationPath = AppDomain.CurrentDomain.BaseDirectory;
            var nativeBinaryPath = IntPtr.Size > 4
                ? Path.Combine(rootApplicationPath, @"SecuGen\x64\")
                : Path.Combine(rootApplicationPath, @"SecuGen\x86\");

            var files = System.IO.Directory.GetFiles(nativeBinaryPath);
            foreach (var file in files)
            {
                var filename = Path.GetFileName(file);
                var nfile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filename);
                if (!File.Exists(nfile))
                {
                    File.Copy(file, nfile);
                }
                //var hLib = Loader.LoadLibrary(file);
            }
        }
    }
}

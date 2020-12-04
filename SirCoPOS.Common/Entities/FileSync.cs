using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Common.Entities
{
    public class FileSync
    {
        public string FileName { get; set; }
        public int FileSize { get; set; }
        public DateTime Date { get; set; }
        public string Checksum { get; set; }
    }
}

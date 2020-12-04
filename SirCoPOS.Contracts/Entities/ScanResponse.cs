using System;
using System.Collections.Generic;
using System.Text;

namespace SirCoPOS.Contracts.Entities
{
    public class ScanResponse
    {
        public int Id { get; set; }
        public string Serie { get; set; }
        public decimal Precio { get; set; }
        public bool HasImage { get; set; }
        public string Status { get; set; }
    }    
}

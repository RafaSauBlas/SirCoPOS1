using System;
using System.Collections.Generic;
using System.Text;

namespace SirCoPOS.App.Models
{
    class Producto
    {
        public int Id { get; set; }
        public string Serie { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public decimal Precio { get; set; }
        public bool HasImage { get; set; }
    }
}

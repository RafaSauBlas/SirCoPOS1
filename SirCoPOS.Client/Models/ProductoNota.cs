using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Client.Models
{
    class ProductoNota : GalaSoft.MvvmLight.ObservableObject
    {
        public Common.Entities.Producto Producto { get; set; }
        private decimal? _precio;
        public decimal? Precio {
            get => _precio;
            set => this.Set(nameof(Precio), ref _precio, value);
        }
        public string Comentarios { get; set; }
    }
}

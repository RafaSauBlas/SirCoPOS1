using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Client.Models
{
    public class CancelProducto : GalaSoft.MvvmLight.ObservableObject
    {
        private bool _scanned;
        public bool Scanned {
            get { return _scanned; }
            set { this.Set(nameof(this.Scanned), ref _scanned, value); }
        }
        public Common.Entities.Producto Producto { get; set; }
    }
}

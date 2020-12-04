using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Client.Models
{
    public class SucursalFolio : GalaSoft.MvvmLight.ObservableObject
    {
        private string _sucursal;
        public string Sucursal
        {
            get { return _sucursal; }
            set { this.Set(nameof(this.Sucursal), ref _sucursal, value); }
        }
        private string _folio;
        public string Folio
        {
            get { return _folio; }
            set { this.Set(nameof(this.Folio), ref _folio, value); }
        }
    }
}

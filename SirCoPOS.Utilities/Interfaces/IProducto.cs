using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Utilities.Interfaces
{
    public interface IProducto
    {
        string Serie { get; set; }
        decimal? Precio { get; set; }
        byte? MaxPlazos { get; set; }
        ObservableCollection<Common.Entities.FormaPagoImporte> FormasPago { get; set; }
    }
}

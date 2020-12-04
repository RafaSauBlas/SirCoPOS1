using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RSG;
using SirCoPOS.Utilities.Models;

namespace SirCoPOS.Utilities.Interfaces
{
    public interface IPago
    {
        string Title { get; }
        Common.Constants.FormaPago FormaPago { get; }
        Guid GID { get; set; }
        decimal Total { get; /*set;*/ }
        decimal? Pagar { get; set; }
        ICajaBase Caja { get; set; }
        int? ClientId { get; set; }
        bool HasPromocionPlazos { get; set; }
        ObservableCollection<ProductoPlazoOpciones> Productos { get; }
        Promise Ready { get; }
    }
}

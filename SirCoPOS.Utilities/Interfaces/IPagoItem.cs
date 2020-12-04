using SirCoPOS.Common.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Utilities.Interfaces
{
    public interface IPagoItem
    {
        FormaPago FormaPago { get; }
        int? ClientId { get; }
        decimal? Importe { get; }
        Guid Id { get; }
        bool HasPromocion { get; set; }
    }
}

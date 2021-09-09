using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Utilities.Interfaces
{
    public interface ICaja : ICajaBase
    {
        Task UpdatePromociones();
        bool SkipPromociones { get; set; }
        string DevSucursal { get; set; }
        string DevFolio { get; set; }

    }
    public interface ICajaBase
    {
        ObservableCollection<IPagoItem> Pagos { get; set; }
        decimal Total { get; }
        decimal Remaining { get; }
        decimal RemainingCalzado { get; }
        decimal RemainingElectronica { get; }
        void UpdatePagos();
        void refreshValorDV();

    }
}

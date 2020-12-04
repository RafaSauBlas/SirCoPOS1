using SirCoPOS.Common.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Client.Models.Pagos
{
    public class Pago : GalaSoft.MvvmLight.ObservableObject, Utilities.Interfaces.IPagoItem
    {
        public Pago()
        {
            if (GalaSoft.MvvmLight.ViewModelBase.IsInDesignModeStatic)
            {
                this.FormaPago = FormaPago.EF;
                this.Importe = 99m;
            }
        }
        public Guid Id { get; set; }
        public FormaPago FormaPago { get; set; }
        private decimal? _importe;
        public decimal? Importe
        {
            get { return _importe; }
            set { this.Set(() => this.Importe, ref _importe, value); }
        }
        public int? ClientId { get; set; }
        private bool _HasPromocion;
        public bool HasPromocion
        {
            get { return _HasPromocion; }
            set { Set(nameof(this.HasPromocion), ref _HasPromocion, value); }
        }

    }
}

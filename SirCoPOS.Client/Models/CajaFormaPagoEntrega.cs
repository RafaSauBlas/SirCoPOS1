using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Client.Models
{
    class CajaFormaPagoEntrega : Utilities.Helpers.EntityBase
    {
        public Common.Entities.CajaFormaPago CajaFormaPago { get; set; }
        private int? _Entregar;
        public int? Entregar
        {
            get { return _Entregar; }
            set { Set(nameof(this.Entregar), ref _Entregar, value); }
        }
        private decimal? _EntregarMonto;
        public decimal? EntregarMonto
        {
            get { return _EntregarMonto; }
            set { Set(nameof(this.EntregarMonto), ref _EntregarMonto, value); }
        }

    }
}

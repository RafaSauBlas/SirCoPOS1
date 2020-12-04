using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Common.Entities
{
    public class Promocion : ObservableObject
    {
        public virtual bool IsCupon => false;
        public int PromocionId { get; set; }
        public string Nombre { get; set; }
        public bool HasCliente { get; set; }

        private bool _enabled;
        public bool Enabled
        {
            get { return _enabled; }
            set { this.Set(nameof(Enabled), ref _enabled, value); }
        }
        private bool _used;
        public bool Used {
            get { return _used; }
            set { this.Set(nameof(Used), ref _used, value); }
        }
    }
}

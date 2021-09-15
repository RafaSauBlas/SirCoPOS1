using SirCoPOS.Common.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Client.Models.Pagos
{
    public class PagoDevolucion : Pago
    {
        public string Sucursal { get; set; }
        public string Folio { get; set; }
        public string Tipo  { get; set; }
        public PagoDevolucion() 
        {
            if (GalaSoft.MvvmLight.ViewModelBase.IsInDesignModeStatic)
            {
                this.Sucursal = "08";
                this.Folio = "024886";
                this.Tipo = "CR";
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SirCoPOS.Common.Entities;

namespace SirCoPOS.BusinessLogic
{
    class VentaP
    {
        public ScanResponse prueba(DataAccess.SirCoPV.Venta pru)
        {
            var ctxpv = new DataAccess.SirCoPVDataContext();
            ctxpv.Ventas.Add(pru);
            ctxpv.SaveChanges();
            return null;
        }
    }
}

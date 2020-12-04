using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.BusinessLogic
{
    public static class Shared
    {
        public static DataAccess.SirCo.Corrida GetCorrida(this DataAccess.SirCoDataContext ctx, DataAccess.SirCo.Serie item)
        {
            var corrida = ctx.Corridas.Where(i => i.marca == item.marca
                && i.estilon == item.estilon
                && i.proveedor == item.proveedors
                && String.Compare(ctx.ValorMedida(item.medida), ctx.ValorMedida(i.medini)) >= 0
                && String.Compare(ctx.ValorMedida(item.medida), ctx.ValorMedida(i.medfin)) <= 0
            ).SingleOrDefault();
            return corrida;
        }
    }
}
